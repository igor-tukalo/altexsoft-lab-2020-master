using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IIngredientsControl : IBaseControl
    {
        List<Ingredient> Ingredients { get; set; }
        List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1);
    }
}