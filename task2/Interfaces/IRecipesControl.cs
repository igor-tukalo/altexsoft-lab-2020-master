using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface IRecipesControl
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