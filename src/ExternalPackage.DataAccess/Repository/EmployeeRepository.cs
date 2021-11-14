using ExternalPackage.DAO;
using ExternalPackage.DataAccess.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExternalPackage.DataAccess.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<EmployeeDao> GetEmployees()
        {
            return _employees;
        }
        public EmployeeDao GetEmployee(long id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<DependentDao> GetEmployeeDependents(long employeeId)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

            _dependentsLookup.TryGetValue(employee.FamilyLinkId, out IEnumerable<DependentDao> dependents);

            return dependents;
        }        



        public long AddEmployee(EmployeeDao employee)
        {
            // package implementation for adding an employee, with the assumption that the insertion statement would return
            // the new id
            return 101;
        }

        public long AddDependent(long employeeId, DependentDao dependent)
        {
            // package implementation for adding a dependent, with the assumption that the insertion statement would return
            // the new id
            return 201;
        }



        public void UpdateEmployee(EmployeeDao employee)
        {
            // package implementation for updating employee with id of param
            return;
        }

        public void UpdateDependent(DependentDao dependent)
        {
            // package implementation for updating dependent with id of param
            return;
        }



        public void DeleteEmployee(long id)
        {
            // package implementation for deletion of employee with id of param
            return;
        }

        public void DeleteDependent(long id)
        {
            // package implementation for deletion of dependent with id of param
            return;
        }


        #region dummy data
        private static readonly List<EmployeeDao> _employees = new List<EmployeeDao>
        {
            new EmployeeDao
            {
                Id = 1,
                NameFirst = "Bilbo",
                NameLast = "Baggins",
                FamilyLinkId = "ABCD1234",
                AnnualSalary = 52000,
            },
            new EmployeeDao
            {
                Id = 2,
                NameFirst = "Arathorn",
                NameLast = "Elessar",
                FamilyLinkId = "QWER4321",
                AnnualSalary = 52000,
            },
            new EmployeeDao
            {
                Id = 3,
                NameFirst = "Morgoth",
                NameLast = "Bauglir",
                FamilyLinkId = "UIOP1337",
                AnnualSalary = 52000,
            },
        };

        private static readonly Dictionary<string, IEnumerable<DependentDao>> _dependentsLookup = new Dictionary<string, IEnumerable<DependentDao>>
        {
            { "ABCD1234", new List<DependentDao>
                {
                    new DependentDao
                    {
                        Id = 1,
                        NameFirst = "Frodo",
                        NameLast = "Baggins",
                        FamilyLinkId = "ABCD1234",
                    },
                    new DependentDao
                    {
                        Id = 2,
                        NameFirst = "Drogo",
                        NameLast = "Baggins",
                        FamilyLinkId = "ABCD1234",
                    },
                }
            },
            { "QWER4321", new List<DependentDao>
                {
                    new DependentDao
                    {
                        Id = 3,
                        NameFirst = "Aragorn",
                        NameLast = "Elessar",
                        FamilyLinkId = "QWER4321",
                    },
                }
            },
            { "UIOP1337", new List<DependentDao>
                {
                    new DependentDao
                    {
                        Id = 4,
                        NameFirst = "Balrog",
                        NameLast = "Demon",
                        FamilyLinkId = "UIOP1337",
                    },
                    new DependentDao
                    {
                        Id = 5,
                        NameFirst = "Sauron",
                        NameLast = "Mairon",
                        FamilyLinkId = "UIOP1337",
                    },
                }
            },
        };

        #endregion
    }
}
