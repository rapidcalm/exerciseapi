using ExerciseApi.Contracts;
using ExerciseApi.Model;
using ExternalPackage.DataAccess.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseApi.Controllers
{
    [Route("api/employees/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBenefitCostDeterminer _benefitCostDeterminer;

        public EmployeeController(IEmployeeRepository employeeRepository, IBenefitCostDeterminer benefitCostDeterminer)
        {
            _employeeRepository = employeeRepository;
            _benefitCostDeterminer = benefitCostDeterminer;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = new List<Employee>();

            var employeesTask = Task.Run(() =>
            {
                // get employees from repo
                var repoEmployees = _employeeRepository.GetEmployees();

                foreach (var edao in repoEmployees)
                {
                    // get employee's dependents and hydrate to employee model
                    var dependents = _employeeRepository.GetEmployeeDependents(edao.Id);

                    var employee = Hydrator.ToModel(edao, dependents);

                    // calculate benefit cost(s) for employee
                    _benefitCostDeterminer.DetermineEmployeeBenefitCosts(employee);

                    employees.Add(employee);
                }
            });

            await Task.WhenAll(employeesTask);


            return Ok(employees);
        }

        // GET api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var repoEmployee = _employeeRepository.GetEmployee(id);
            Employee employee = null;

            var employeeTask = Task.Run(() =>
            {
                // get employee's dependents and hydrate to employee model
                employee = Hydrator.ToModel(repoEmployee, _employeeRepository.GetEmployeeDependents(repoEmployee.Id));

                // calculate benefits cost(s) for employee
                _benefitCostDeterminer.DetermineEmployeeBenefitCosts(employee);
            });

            await Task.WhenAll(employeeTask);

            if (employee is null)
            {
                return BadRequest();
            }

            return Ok(employee);
        }

        // POST api/employees
        [HttpPost]
        public async Task<ActionResult<long>> AddEmployee([FromBody]Employee employee)
        {
            // assumption that adding a new employee would 
            long newEmployeeId = 0;

            var newEmployeeTask = Task.Run(() =>
            {
                newEmployeeId = _employeeRepository.AddEmployee(Hydrator.FromModel(employee));
            });

            await Task.WhenAll(newEmployeeTask);

            if (newEmployeeId == 0)
            {
                return BadRequest();
            }

            return Ok(newEmployeeId);
        }

        // PUT api/employees/{id}
        [HttpPut("{id}")]        
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var updateEmployeeTask = Task.Run(() =>
            {
                _employeeRepository.UpdateEmployee(Hydrator.FromModel(employee));
            });

            await Task.WhenAll(updateEmployeeTask);

            return Ok(employee);
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var deleteEmployeeTask = Task.Run(() =>
            {
                _employeeRepository.DeleteEmployee((long)id);
            });

            await Task.WhenAll(deleteEmployeeTask);

            return Ok(id);
        }

        
    }
}
