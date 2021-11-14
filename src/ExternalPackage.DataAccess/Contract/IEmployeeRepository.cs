using ExternalPackage.DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalPackage.DataAccess.Contract
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDao> GetEmployees();
        EmployeeDao GetEmployee(long id);
        IEnumerable<DependentDao> GetEmployeeDependents(long employeeId);


        long AddEmployee(EmployeeDao employee);
        long AddDependent(long employeeId, DependentDao dependent);        



        void UpdateEmployee(EmployeeDao employee);
        void UpdateDependent(DependentDao dependent);


        void DeleteEmployee(long id);
        void DeleteDependent(long id);
    }
}
