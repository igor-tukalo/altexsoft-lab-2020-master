using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface IRecipesControl
    {
        void Add(int idCategory);
        void Edit(int id);
        void Delete(int id);
        void BuildRecipesCategories(List<EntityMenu> items, Category thisEntity, int level, int levelLimitation);
        int GetIdCategory(int idRecipe);
        void View(int idRecipe);
        void ChangeDescription(int idRecipe);
    }
}
