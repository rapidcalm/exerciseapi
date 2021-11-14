using ExerciseApi.Contracts;
using ExerciseApi.Model;
using ExternalPackage.DataAccess.Contract;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<long> AddDependent(int id, [FromBody]Dependent dependent)
        {
            // assumption that adding a new dependent would return its id upon insertion 
            var newDependentId = _employeeRepository.AddDependent((long) id, Hydrator.FromModel(dependent));

            return newDependentId;
        }

        // PUT api/dependents/{id}
        [HttpPut("{id}")]
        public ActionResult<Dependent> UpdateDependent(int id, [FromBody] Dependent dependent)
        {
            if (id != dependent.Id)
            {
                return BadRequest();
            }

            _employeeRepository.UpdateDependent(Hydrator.FromModel(dependent));

            return dependent;
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeRepository.DeleteDependent((long)id);
        }
    }
}
