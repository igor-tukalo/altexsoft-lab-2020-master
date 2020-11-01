using HomeTask4.SharedKernel;
using System.Collections.Generic;

namespace HomeTask4.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public List<AmountIngredient> AmountIngredients { get; set; }
    }
}
