namespace task2.Models
{
    public class Ingredient : EntityMenu
    {
        public int IdRecipe { get; set; }

        public Ingredient(int id = 0, string name = "", int? parentId = 0) : base(id, name, parentId)
        {
        }

    }
}
