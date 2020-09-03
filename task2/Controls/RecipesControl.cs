using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class RecipesControl : BaseControl, IRecipesControl
    {
        public RecipesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public int GetIdCategory(int idRecipe)
        {
            return UnitOfWork.Recipes.Get(idRecipe).IdCategory;
        }
        public void View(int idRecipe)
        {
            var recipe = UnitOfWork.Recipes.Get(idRecipe);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {Validation.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            //ingredients recipe
                foreach (var a in UnitOfWork.AmountIngredients.Items.Where(x => x.IdRecipe == recipe.Id))
                {
                    foreach (var i in UnitOfWork.Ingredients.Items.Where(x => x.Id == a.IdIngredient))
                    {
                        Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                    }
                }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (var s in UnitOfWork.CookingSteps.Items.Where(x => x.IdRecipe == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {Validation.WrapText(10, s.Name, "\n       ")}");
            }
        }

        public void Edit(int id)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = UnitOfWork.Recipes.IsNameMustNotExist(Console.ReadLine());
            var recipe = UnitOfWork.Recipes.Get(id);
            recipe.Name = nameRecipe;
            UnitOfWork.Recipes.Update(recipe);
            UnitOfWork.SaveAllData();
        }

        public void ChangeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            var recipe = UnitOfWork.Recipes.Get(idRecipe);
            recipe.Description = description;
            UnitOfWork.Recipes.Update(recipe);
            UnitOfWork.SaveAllData();
        }

        public void Add(int idCategory)
        {
            int idRecipe = UnitOfWork.Recipes.Items.Count() > 0 ? UnitOfWork.Recipes.Items.Max(x => x.Id) + 1 : 1;
            Console.WriteLine($"\n    The recipe will be added to the category: {UnitOfWork.Categories.Get(idCategory).Name}");
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = UnitOfWork.Recipes.IsNameMustNotExist(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.Recipes.Create(new Recipe() { Id = idRecipe, Name = nameRecipe, Description=description, IdCategory = idCategory });
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            Console.Clear();
            Console.Write("Are you sure you want to delete the recipe? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            UnitOfWork.AmountIngredients.Items.RemoveAll(r => r.IdRecipe == id);
            UnitOfWork.CookingSteps.Items.RemoveAll(r => r.IdRecipe == id);
            UnitOfWork.Recipes.Delete(id);
            UnitOfWork.SaveAllData();
        }

        /// <summary>
        ///  Get recipes categories
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="thisEntity">parent element</param>
        /// <param name="level">level hierarchy</param>
        /// <param name="levelLimitation">level limitation hierarchy</param>
        public void BuildRecipesCategories(List<EntityMenu> items, Category thisEntity, int level, int levelLimitation)
        {
            if (level > levelLimitation)
                return;
            items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
            foreach (var recipe in UnitOfWork.Recipes.Items.Where(x => x.IdCategory == thisEntity.Id))
            {
                items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.IdCategory, TypeEntity = "recipe" });
            }
            foreach (var child in UnitOfWork.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                var entityMenu = new EntityMenu() { Id = child.Id, Name = child.Name, ParentId = child.ParentId };
                BuildCurrentOpenRecipesCategories(items, entityMenu, level + 1, levelLimitation);
            }
        }

        /// <summary>
        /// Get recipes for the currently open category only
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="thisEntity">item which have ParentId 0</param>
        /// <param name="level">level hierarchy</param>
        /// <param name="levelLimitation">level limitation hierarchy</param>
        private void BuildCurrentOpenRecipesCategories(List<EntityMenu> items, EntityMenu thisEntity, int level, int levelLimitation)
        {
            if (level <= levelLimitation)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });

                foreach (var child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Id))
                {
                    BuildCurrentOpenRecipesCategories(items, child, level + 1, levelLimitation);
                }
            }
        }
    }
}
