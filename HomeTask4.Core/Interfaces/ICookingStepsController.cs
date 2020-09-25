using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface ICookingStepsController
    {
        void Add(int idRecipe);
        void Edit(int id);
        void Delete(int id, int idRecipe);
        List<EntityMenu> GetItems(List<EntityMenu> itemsMenu, int idRecipe);
    }
}
