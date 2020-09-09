namespace task2.Models
{
    class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
