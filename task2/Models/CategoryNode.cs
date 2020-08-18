using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    public class CategoryNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryNode> Children { get; set; }

        public CategoryNode()
        {
            Children = new List<CategoryNode>();
        }
    }
}
