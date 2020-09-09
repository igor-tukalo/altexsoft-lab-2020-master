using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface ICookingStepsControl
    {
        void Add(int idRecipe);
        void Edit(int id);
        void Delete(int id, int idRecipe);
        List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe);
    }
}