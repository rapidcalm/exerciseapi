using ExerciseApi.Configuration;
using ExerciseApi.Contracts;
using ExerciseApi.Model;
using System;

namespace ExerciseApi.Rule
{
    public class BenefitCostDeterminer : IBenefitCostDeterminer
    {
        public BenefitCostConfiguration BenefitCostConfiguration { get; }

        public BenefitCostDeterminer(BenefitCostConfiguration benefitCostConfig)
        {
            BenefitCostConfiguration = benefitCostConfig;
        }

        public void DetermineEmployeeBenefitCosts(Employee employee)
        {
            // calculate the employee's benefit cost
            CalculateMemberPeriodBenefitCost(employee);

            // start the total benefit period cost
            double totalPeriodCost = employee.EmployeePeriodBenefitCost;

            // calculate the cost for each dependent and add it to the total
            foreach(var dependent in employee.Dependents)
            {
                CalculateMemberPeriodBenefitCost(dependent);
                
                totalPeriodCost += dependent.PayPeriodBenefitCost;
            }

            employee.TotalPeriodBenefitCost = Math.Round(totalPeriodCost, 2);
        }

        /// <summary>
        /// Calculates and applies a single employee or dependent's benefit cost for one pay period.
        /// </summary>        
        protected void CalculateMemberPeriodBenefitCost(Member member)
        {
            var applyDiscount = ShouldApplyDiscount(member);

            if (member is Employee employee)
            {
                var payPeriodBeneCost = (double)BenefitCostConfiguration.AnnualEmployeeBenefitCost / BenefitCostConfiguration.PayPeriodCount;
                payPeriodBeneCost = applyDiscount ? payPeriodBeneCost * BenefitCostConfiguration.DiscountMultiplier : payPeriodBeneCost;
                employee.EmployeePeriodBenefitCost = Math.Round(payPeriodBeneCost, 2);

            }
            else if (member is Dependent dependent)
            {
                var payPeriodBeneCost = (double)BenefitCostConfiguration.AnnualDependentBenefitCost / BenefitCostConfiguration.PayPeriodCount;
                payPeriodBeneCost = applyDiscount ? payPeriodBeneCost * BenefitCostConfiguration.DiscountMultiplier : payPeriodBeneCost;
                dependent.PayPeriodBenefitCost = Math.Round(payPeriodBeneCost, 2);
            }
            else
            {
                throw new System.Exception("Attempt to determine benefit cost on undetermined member type.");
            }
        }

        

        /// <summary>
        /// Abstraction to allow for expansion
        /// </summary>
        protected bool ShouldApplyDiscount(Member member)
        {
            return BenefitCostConfiguration.DiscountByFirstName && member.FirstName.StartsWith(BenefitCostConfiguration.NameStartsWith, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
