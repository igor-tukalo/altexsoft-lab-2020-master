using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    public class AmountRecipeIngredient
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public int IdRecipe { get; set; }
        public int IdIngredient { get; set; }
    }
}
