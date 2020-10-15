namespace HomeTask6.Web.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
