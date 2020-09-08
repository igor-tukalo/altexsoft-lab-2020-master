using System.Collections.Generic;

namespace task2.Models
{
    class CategoryNode : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<CategoryNode> Children { get; set; }

        public CategoryNode()
        {
            Children = new List<CategoryNode>();
        }
    }
}
