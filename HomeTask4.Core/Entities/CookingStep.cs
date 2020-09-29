using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class CookingStep : BaseEntity
    {
        public string Name { get; set; }
        public int Step { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
