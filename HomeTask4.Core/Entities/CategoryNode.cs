using HomeTask4.SharedKernel;
using System.Collections.Generic;

namespace HomeTask4.Core.Entities
{
    public class CategoryNode : BaseEntity
    {
        public string Name { get; set; }
        public List<CategoryNode> Children { get; }

        public CategoryNode()
        {
            Children = new List<CategoryNode>();
        }
    }
}
