using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface ICategoriesControl : IBaseControl
    {
        void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level);
        void RemoveHierarchicalCategory(Category thisEntity, int level);
        Category GetParentCategory(int id);
    }
}
