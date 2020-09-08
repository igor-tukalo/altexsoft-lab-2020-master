namespace task2.Models
{
    class Recipe : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }

    }
}
