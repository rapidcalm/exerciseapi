using ExerciseApi.Configuration;
using ExerciseApi.Model;

namespace ExerciseApi.Contracts
{
    public interface IBenefitCostDeterminer
    {
        BenefitCostConfiguration BenefitCostConfiguration { get; }

        void DetermineEmployeeBenefitCosts(Employee employee);
    }
}
