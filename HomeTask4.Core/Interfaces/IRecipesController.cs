using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface IRecipesController
    {
        void Add(int idCategory);
        void Edit(int id);
        void Delete(int id);
        void BuildRecipesCategories(List<EntityMenu> items, Category thisEntity, int level, int levelLimitation);
        void View(int idRecipe);
        void ChangeDescription(int idRecipe);
    }
}
