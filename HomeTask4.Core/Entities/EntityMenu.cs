using HomeTask4.SharedKernel;

namespace HomeTask4.Core.Entities
{
    public class EntityMenu : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string TypeEntity { get; set; }
    }
}
