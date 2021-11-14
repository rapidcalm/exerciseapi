namespace ExerciseApi.Configuration
{
    public class DiscountOptions
    {
        public const string Discount = "Discount";

        // discount types? would require new implementation with any new discount type, but would allow for configuration after
        public bool ByFirstName { get; set; } = false;
        public string NameStartsWith { get; set; } = string.Empty;
        public int DiscountPercentage { get; set; } = 0;
    }
}
