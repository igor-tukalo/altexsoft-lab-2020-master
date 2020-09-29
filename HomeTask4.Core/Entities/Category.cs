using HomeTask4.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace HomeTask4.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
