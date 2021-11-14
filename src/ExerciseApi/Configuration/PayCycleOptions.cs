namespace ExerciseApi.Configuration
{
    public class PayCycleOptions
    {
        public const string PayCycle = "PayCycle";

        public int AnnualPayPeriodCount { get; set; } = 26; // default to two weeks/year
    }
}
