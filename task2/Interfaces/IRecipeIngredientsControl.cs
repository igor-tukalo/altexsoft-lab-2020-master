using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IRecipeIngredientsControl : IBaseControl
    {
        List<AmountIngredient> AmountIngredients { get; set; }
        List<Ingredient> Ingredients { get; set; }
        int IdRecipe { get; set; }
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
        void Add(int idIngredient);
    }
}