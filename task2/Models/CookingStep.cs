namespace task2.Models
{
    class CookingStep : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Step { get; set; }
        public int IdRecipe { get; set; }
    }
}
