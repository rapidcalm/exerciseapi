using ExerciseApi.Contracts;
using ExerciseApi.Model;
using ExternalPackage.DataAccess.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = new List<Employee>();
            
            // get employees from repo
            var repoEmployees = _employeeRepository.GetEmployees();
            
            foreach(var edao in repoEmployees)
            {
                // get employee's dependents and hydrate to employee model
                var dependents = _employeeRepository.GetEmployeeDependents(edao.Id);
                
                var employee = Hydrator.ToModel(edao, dependents);

                // calculate benefit cost(s) for employee
                _benefitCostDeterminer.DetermineEmployeeBenefitCosts(employee);

                employees.Add(employee);
            }


            return employees;
        }

        // GET api/employees/{id}
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var repoEmployee = _employeeRepository.GetEmployee(id);

            // get employee's dependents and hydrate to employee model
            var employee = Hydrator.ToModel(repoEmployee, _employeeRepository.GetEmployeeDependents(repoEmployee.Id));

            // calculate benefits cost(s) for employee
            _benefitCostDeterminer.DetermineEmployeeBenefitCosts(employee);

            return employee;
        }

        // POST api/employees
        [HttpPost]
        public ActionResult<long> AddEmployee([FromBody]Employee employee)
        {
            // assumption that adding a new employee would 
            var newEmployeeId = _employeeRepository.AddEmployee(Hydrator.FromModel(employee));

            return newEmployeeId;
        }

        // PUT api/employees/{id}
        [HttpPut("{id}")]        
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            
            _employeeRepository.UpdateEmployee(Hydrator.FromModel(employee));

            return employee;
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeRepository.DeleteEmployee((long)id);
        }

        
    }
}
