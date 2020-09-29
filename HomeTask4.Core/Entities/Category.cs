using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
