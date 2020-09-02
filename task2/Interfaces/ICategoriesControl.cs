using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    public interface ICategoriesControl : IBaseControl
    {
        void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level);
        void RemoveHierarchicalCategory(Category thisEntity, int level);
        Category GetParentCategory(int id);
    }
}