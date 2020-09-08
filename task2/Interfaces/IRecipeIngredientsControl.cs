using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IRecipeIngredientsControl : IBaseControl
    {
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
        void Add(int idIngredient, int idRecipe);
    }
}