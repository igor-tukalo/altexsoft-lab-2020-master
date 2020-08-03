using System;
using System.Collections.Generic;
using System.Text;

namespace task2.Models
{
    public class AmountRecipeIngredient
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public int IdRecipe { get; set; }
        public int IdIngredient { get; set; }
    }
}
