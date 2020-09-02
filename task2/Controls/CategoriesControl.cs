using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    public class CategoriesControl : BaseControl, ICategoriesControl
    {
        public Category GetParentCategory(int id)
        {
            return DBControl.Categories.Get(id);
        }

        public override void Add()
        {
            int id = DBControl.Categories.Items.Count() > 0 ? DBControl.Categories.Items.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter name category: ");
            string name = Console.ReadLine();
            Console.Write("    Enter name main category: ");
            string nameMainCategory = DBControl.Categories.IsNameMustExist(Validation.NullOrEmptyText(Console.ReadLine()));
            int idMainCategory = (from t in DBControl.Categories.Items
                                  where t.Name == nameMainCategory
                                  select t.Id).First();
            DBControl.Categories.Create(new Category() { Id = id, Name = name, ParentId = idMainCategory });
            base.Add();
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            foreach (var child in DBControl.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalCategories(items, child, level + 1);
            }
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            foreach (var r in DBControl.Recipes.Items.Where(x => x.IdCategory == thisEntity.Id))
            {
                DBControl.AmountIngredients.Items.RemoveAll(x => x.IdRecipe == r.Id);
                DBControl.CookingSteps.Items.RemoveAll(x => x.IdRecipe == r.Id);
            }
            DBControl.Recipes.Items.RemoveAll(x => x.IdCategory == thisEntity.Id);
            DBControl.Categories.Delete(thisEntity.Id);
            foreach (var child in DBControl.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(child, level + 1);
            }
        }

        public override void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = DBControl.Categories.IsNameMustNotExist(Console.ReadLine());
            var category = DBControl.Categories.Get(id);
            category.Name = newName;
            DBControl.Categories.Update(category);
            base.Edit(id);
        }

        public override void Delete(int id)
        {
            if (GetParentCategory(id).ParentId == 0) return;
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            var parent = DBControl.Categories.Get(id);
            RemoveHierarchicalCategory(parent, 1);
            base.Delete(id);
        }
    }
}
