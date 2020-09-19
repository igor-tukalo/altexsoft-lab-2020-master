using HomeTask4.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace HomeTask4.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
