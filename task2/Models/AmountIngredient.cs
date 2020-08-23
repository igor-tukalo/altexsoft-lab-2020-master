namespace task2.Models
{
    public class AmountIngredient
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int IdRecipe { get; set; }
        public int IdIngredient { get; set; }
    }
}
