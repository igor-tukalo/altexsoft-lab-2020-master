namespace task2.Models
{
    public class Category : EntityMenu
    {
        public Category(int id = 0, string name = "", int? parentId = 0, string typeEntity = "") : base(id, name, parentId,typeEntity)
        {
        }
    }
}
