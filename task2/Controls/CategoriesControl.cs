using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class CategoriesControl : BaseCategoriesRecipesControl, ICategoriesControl
    {
        public CategoriesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetParentCategory(int id)
        {
            return CategoryRepository.Get(id);
        }

        public void Add()
        {
            int id = CategoryRepository.Items.Count() > 0 ? CategoryRepository.Items.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter name category: ");
            string name = Console.ReadLine();
            Console.Write("    Enter name main category: ");
            string nameMainCategory = CategoryRepository.IsNameMustExist(Validation.NullOrEmptyText(Console.ReadLine()));
            int idMainCategory = (from t in CategoryRepository.Items
                                  where t.Name == nameMainCategory
                                  select t.Id).First();
            CategoryRepository.Create(new Category() { Id = id, Name = name, ParentId = idMainCategory });
            UnitOfWork.SaveAllData();
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            foreach (var child in CategoryRepository.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
                BuildHierarchicalCategories(items, child, level + 1);
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            foreach (var r in RecipeRepository.Items.Where(x => x.IdCategory == thisEntity.Id))
            {
                AamountIngredientRepository.Items.RemoveAll(x => x.IdRecipe == r.Id);
                CookingStepRepository.Items.RemoveAll(x => x.IdRecipe == r.Id);
            }
            RecipeRepository.Items.RemoveAll(x => x.IdCategory == thisEntity.Id);
            CategoryRepository.Delete(thisEntity.Id);
            foreach (var child in CategoryRepository.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
                RemoveHierarchicalCategory(child, level + 1);
        }

        public void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = CategoryRepository.IsNameMustNotExist(Console.ReadLine());
            var category = CategoryRepository.Get(id);
            category.Name = newName;
            CategoryRepository.Update(category);
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            if (GetParentCategory(id).ParentId == 0) return;
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            var parent = CategoryRepository.Get(id);
            RemoveHierarchicalCategory(parent, 1);
            UnitOfWork.SaveAllData();
        }
    }
}
