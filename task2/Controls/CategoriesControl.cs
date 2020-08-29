using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class CategoriesControl : ICategoriesControl
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public Category GetParentCategory(int id)
        {
            return unitOfWork.Categories.Get(id);
        }

        public void Add()
        {
            try
            {
                int id = unitOfWork.Categories.GetAll().Max(x => x.Id) + 1;
                Console.Write("\n    Enter name category: ");
                string name = Console.ReadLine();
                Console.Write("    Enter name main category: ");
                string nameMainCategory = unitOfWork.Categories.IsNameMustExist(Validation.NullOrEmptyText(Console.ReadLine()));
                int idMainCategory = (from t in unitOfWork.Categories.GetAll()
                                      where t.Name == nameMainCategory
                                      select t.Id).First();
                unitOfWork.Categories.Create(new Category() { Id = id, Name = name, ParentId = idMainCategory });
                unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }

        public void BuildHierarchicalCategories(List<EntityMenu> items, Category thisEntity, int level)
        {
            items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            foreach (var child in unitOfWork.Categories.GetAll().FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalCategories(items, child, level + 1);
            }
        }

        public void RemoveHierarchicalCategory(Category thisEntity, int level)
        {
            unitOfWork.Categories.Delete(thisEntity.Id);
            foreach (var r in unitOfWork.Recipes.GetAll().ToList().Where(x => x.IdCategory == thisEntity.Id))
            {
                foreach (var a in unitOfWork.AmountIngredients.GetAll().ToList().Where(x => x.IdRecipe == r.Id))
                    unitOfWork.AmountIngredients.Delete(a.Id);

                foreach (var a in unitOfWork.CookingSteps.GetAll().ToList().Where(x => x.IdRecipe == r.Id))
                    unitOfWork.CookingSteps.Delete(a.Id);

                unitOfWork.Recipes.Delete(r.Id);
            }
            foreach (var child in unitOfWork.Categories.GetAll().FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                RemoveHierarchicalCategory(child, level + 1);
            }
        }

        public void Rename(int idCategory)
        {
            Console.Write("    Enter new name: ");
            string newName = unitOfWork.Categories.IsNameMustNotExist(Console.ReadLine());
            var category = unitOfWork.Categories.Get(idCategory);
            category.Name = newName;
            unitOfWork.Categories.Update(category);
            unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
        }

        public void Delete(int idCategory)
        {
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                var parent = unitOfWork.Categories.Get(idCategory);
                RemoveHierarchicalCategory(parent, 1);
                unitOfWork.SaveChangesRecipe();
                unitOfWork.SaveDataTable("Categories.json", JsonConvert.SerializeObject(unitOfWork.Categories.GetAll()));
            }
        }
    }
}
