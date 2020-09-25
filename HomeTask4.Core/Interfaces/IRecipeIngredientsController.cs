using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface IRecipeIngredientsController
    {
        List<EntityMenu> GetItems(List<EntityMenu> itemsMenu, int idRecipe);
        void Add(int idIngredient, int idRecipe);
        void Delete(int id);
    }
}