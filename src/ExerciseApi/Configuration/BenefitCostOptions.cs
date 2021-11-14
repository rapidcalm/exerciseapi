namespace ExerciseApi.Configuration
{
    public class BenefitCostOptions
    {
        public const string BenefitCost = "BenefitCost";

        public int AnnualEmployeeCost { get; set; } = 0;
        public int AnnualDependentCost { get; set; } = 0;
    }
}
