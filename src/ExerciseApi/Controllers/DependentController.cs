using ExerciseApi.Contracts;
using ExerciseApi.Model;
using ExternalPackage.DataAccess.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExerciseApi.Controllers
{
    [Route("api/dependents/")]
    [ApiController]
    public class DependentController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public DependentController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // POST api/dependents/{id}
        [HttpPost("{id}")]
        public async Task<ActionResult<long>> AddDependent(int id, [FromBody]Dependent dependent)
        {
            long newDependentId = 0;
            
            // assumption that adding a new dependent would return its id upon insertion 
            var newDependentTask = Task.Run(() =>
            {
                newDependentId = _employeeRepository.AddDependent((long)id, Hydrator.FromModel(dependent));
            });

            await Task.WhenAll(newDependentTask);

            if (newDependentId == 0)
            {
                return BadRequest();
            }

            return Ok(newDependentId);
        }

        // PUT api/dependents/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Dependent>> UpdateDependent(int id, [FromBody] Dependent dependent)
        {
            if (id != dependent.Id)
            {
                return BadRequest();
            }

            var updateDependentTask = Task.Run(() =>
            {
                _employeeRepository.UpdateDependent(Hydrator.FromModel(dependent));
            });

            await Task.WhenAll(updateDependentTask);

            return Ok(dependent);
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var deleteDependentTask = Task.Run(() =>
            {
                _employeeRepository.DeleteDependent((long)id);
            });

            await Task.WhenAll(deleteDependentTask);
             
            return Ok(id);
        }
    }
}
