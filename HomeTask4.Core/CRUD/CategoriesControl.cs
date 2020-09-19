using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.CRUD
{
    public class CategoriesControl : BaseCategoriesRecipesControl, ICategoriesControl
    {
        public CategoriesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetParentCategory(int id)
        {
            return CategoryRepository.GetItem(id);
        }

        public void Add()
        {
            Console.Write("\n    Enter name category: ");
            string name = Console.ReadLine();
            Console.Write("    Enter name main category: ");
            string nameMainCategory = CategoryRepository.IsNameMustExist(ValidManager.NullOrEmptyText(Console.ReadLine()));
            int idMainCategory = (from t in CategoryRepository.Items
                                  where t.Name == nameMainCategory
                                  select t.Id).First();
            CategoryRepository.Create(new Category() { Name = name, ParentId = idMainCategory });
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            if (items != null && thisEntity != null)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            }
            foreach (Category child in CategoryRepository.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalCategories(items, child, level + 1);
            }
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            if (thisEntity != null)
            {
                CategoryRepository.Delete(CategoryRepository.GetItem(thisEntity.Id));
            }
            foreach (Category child in CategoryRepository.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(child, level + 1);
            }
        }

        public void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = CategoryRepository.IsNameMustNotExist(Console.ReadLine());
            Category category = CategoryRepository.GetItem(id);
            category.Name = newName;
            CategoryRepository.Update(category);
        }

        public void Delete(int id)
        {
            if (GetParentCategory(id).ParentId == 0)
            {
                return;
            }
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            Category parent = CategoryRepository.GetItem(id);
            RemoveHierarchicalCategory(parent, 1);
        }
    }
}
