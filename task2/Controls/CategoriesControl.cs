using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class CategoriesControl : BaseControl, ICategoriesControl
    {
        public CategoriesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public Category GetParentCategory(int id)
        {
            return UnitOfWork.Categories.Get(id);
        }

        public void Add()
        {
            int id = UnitOfWork.Categories.Items.Count() > 0 ? UnitOfWork.Categories.Items.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter name category: ");
            string name = Console.ReadLine();
            Console.Write("    Enter name main category: ");
            string nameMainCategory = UnitOfWork.Categories.IsNameMustExist(Validation.NullOrEmptyText(Console.ReadLine()));
            int idMainCategory = (from t in UnitOfWork.Categories.Items
                                  where t.Name == nameMainCategory
                                  select t.Id).First();
            UnitOfWork.Categories.Create(new Category() { Id = id, Name = name, ParentId = idMainCategory });
            UnitOfWork.SaveAllData();
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            foreach (var child in UnitOfWork.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalCategories(items, child, level + 1);
            }
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            foreach (var r in UnitOfWork.Recipes.Items.Where(x => x.IdCategory == thisEntity.Id))
            {
                UnitOfWork.AmountIngredients.Items.RemoveAll(x => x.IdRecipe == r.Id);
                UnitOfWork.CookingSteps.Items.RemoveAll(x => x.IdRecipe == r.Id);
            }
            UnitOfWork.Recipes.Items.RemoveAll(x => x.IdCategory == thisEntity.Id);
            UnitOfWork.Categories.Delete(thisEntity.Id);
            foreach (var child in UnitOfWork.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(child, level + 1);
            }
        }

        public void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = UnitOfWork.Categories.IsNameMustNotExist(Console.ReadLine());
            var category = UnitOfWork.Categories.Get(id);
            category.Name = newName;
            UnitOfWork.Categories.Update(category);
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            if (GetParentCategory(id).ParentId == 0) return;
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            var parent = UnitOfWork.Categories.Get(id);
            RemoveHierarchicalCategory(parent, 1);
            UnitOfWork.SaveAllData();
        }
    }
}
