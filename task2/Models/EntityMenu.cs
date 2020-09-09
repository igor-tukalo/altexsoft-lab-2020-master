namespace task2.Models
{
    class EntityMenu : BaseEntity<int>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string TypeEntity { get; set; }
    }
}
