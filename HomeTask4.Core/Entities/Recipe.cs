using HomeTask4.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace HomeTask4.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
