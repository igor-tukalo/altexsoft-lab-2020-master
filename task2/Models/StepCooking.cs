namespace task2.Models
{
    public class StepCooking :EntityMenu
    {
        public StepCooking(int id = 0, string name = "", int? parentId = 0, string typeEntity = "") : base(id, name, parentId, typeEntity)
        {
        }

        public int Step { get; set; }
        public int IdRecipe { get; set; }
    }
}
