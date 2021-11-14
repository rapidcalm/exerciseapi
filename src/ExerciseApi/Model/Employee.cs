using System.Collections.Generic;

namespace ExerciseApi.Model
{
    public class Employee : Member
    {
        public int AnnualSalary { get; set; }
        public double EmployeePeriodBenefitCost { get; set; }
        public double TotalPeriodBenefitCost { get; set; }


        public List<Dependent> Dependents { get; set; }

        public Employee()
        {
            Dependents = new List<Dependent>();
        }

        public Employee(IEnumerable<Dependent> dependents) : this()
        {
            Dependents.AddRange(dependents);
        }
    }
}
