namespace task2.Models
{
    class AmountIngredient : BaseEntity<int>
    {
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int IdRecipe { get; set; }
        public int IdIngredient { get; set; }
    }
}
