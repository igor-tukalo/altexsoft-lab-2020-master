using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class AmountIngredient : BaseEntity
    {
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
