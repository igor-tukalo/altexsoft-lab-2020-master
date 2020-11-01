using HomeTask4.SharedKernel;
using System.Collections.Generic;

namespace HomeTask4.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<AmountIngredient> AmountIngredients { get; set; }
        public List<CookingStep> CookingSteps { get; set; }
    }
}
