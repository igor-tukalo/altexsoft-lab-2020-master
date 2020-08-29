using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IRecipeIngredientsControl
    {
        int IdRecipe { get; set; }
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
        void Add(int idRecipeint, int idIngredient);
        void Delete(int idRecipe);
    }
}