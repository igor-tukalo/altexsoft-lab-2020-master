using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    public interface ICategoriesControl
    {
        void Add();
        void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level);
        void Delete(int idCategory);
        void RemoveHierarchicalCategory(Category thisEntity, int level);
        void Rename(int idCategory);
        Category GetParentCategory(int id);
    }
}