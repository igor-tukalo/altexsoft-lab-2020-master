using System.Collections.Generic;

namespace HomeTask4.SharedKernel
{
    public class CategoryTree
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<CategoryTree> Childrens { get; set; }
    }
}
