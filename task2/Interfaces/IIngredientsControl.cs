using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IIngredientsControl
    {
        void Add();
        void Delete(int idIngredient);
        List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1);
        void Rename(int idIngredient);
    }
}