using ExerciseApi.Model;
using ExternalPackage.DAO;
using System.Collections.Generic;
using System.Linq;

namespace ExerciseApi.Controllers
{
    public static class Hydrator
    {
        public static Dependent ToModel(this DependentDao dao)
        {
            if (dao is null)
            {
                return null;
            }

            return new Dependent
            {
                Id = dao.Id,                
                FirstName = dao.NameFirst,
                LastName = dao.NameLast,
            };
        }

        public static Employee ToModel(this EmployeeDao dao, IEnumerable<DependentDao> dependentDaos)
        {
            if (dao is null)
                return null;
            
            return new Employee(dependentDaos.Select(ToModel))
            {
                Id = dao.Id,                
                FirstName = dao.NameFirst,
                LastName = dao.NameLast,
                AnnualSalary = dao.AnnualSalary,
            };
        }

        public static EmployeeDao FromModel(this Employee model)
        {
            if (model is null)
                return null;

            return new EmployeeDao
            {
                Id = model.Id,
                NameFirst = model.FirstName,
                NameLast = model.LastName,
                AnnualSalary = model.AnnualSalary,
            };
        }

        public static DependentDao FromModel(this Dependent model)
        {
            if (model is null)
                return null;

            return new DependentDao
            {
                Id = model.Id,
                NameFirst = model.FirstName,
                NameLast = model.LastName,                
            };
        }
    }
}
