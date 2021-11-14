namespace ExerciseApi.Configuration
{
    public class BenefitCostConfiguration
    {
        // pay cycle options
        public int PayPeriodCount { get; set; }
        
        
        // benefit cost options
        public int AnnualEmployeeBenefitCost { get; set; }
        public int AnnualDependentBenefitCost { get; set; }

                
        // discount options        
        public bool DiscountByFirstName { get; set; }
        public string NameStartsWith { get; set; }
        public double DiscountMultiplier { get; set; }
    }
}
