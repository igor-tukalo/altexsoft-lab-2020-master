using HomeTask4.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace HomeTask4.Core.Entities
{
    public class CookingStep : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int Step { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
