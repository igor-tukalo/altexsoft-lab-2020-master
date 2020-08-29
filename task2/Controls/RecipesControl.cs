using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    class RecipesControl : IRecipesControl
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public int GetIdCategory(int idRecipe)
        {
            return unitOfWork.Recipes.Get(idRecipe).IdCategory;
        }
        public void View(int idRecipe)
        {
            var recipe = unitOfWork.Recipes.Get(idRecipe);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {Validation.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            //ingredients recipe
            if (unitOfWork.AmountIngredients.GetAll() != null)
                foreach (var a in unitOfWork.AmountIngredients.GetAll().Where(x => x.IdRecipe == recipe.Id))
                {
                    foreach (var i in unitOfWork.Ingredients.GetAll().Where(x => x.Id == a.IdIngredient))
                    {
                        Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                    }
                }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (var s in unitOfWork.CookingSteps.GetAll().Where(x => x.IdRecipe == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {Validation.WrapText(10, s.Name, "\n       ")}");
            }
        }

        public void Rename(int idRecipe)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = unitOfWork.Recipes.IsNameMustNotExist(Console.ReadLine());
            var recipe = unitOfWork.Recipes.Get(idRecipe);
            recipe.Name = nameRecipe;
            unitOfWork.Recipes.Update(recipe);
            unitOfWork.SaveDataTable("Recipes.json", JsonConvert.SerializeObject(unitOfWork.Recipes.GetAll()));
        }

        public void ChangeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            var recipe = unitOfWork.Recipes.Get(idRecipe);
            recipe.Description = description;
            unitOfWork.Recipes.Update(recipe);
            unitOfWork.SaveDataTable("Recipes.json", JsonConvert.SerializeObject(unitOfWork.Recipes.GetAll()));
        }

        public void Add(int idCategory)
        {
            Console.WriteLine($"\n    The recipe will be added to the category: {unitOfWork.Categories.Get(idCategory).Name}");
            int idRecipe = unitOfWork.Recipes.GetAll().Count() > 0 ? unitOfWork.Recipes.GetAll().Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = unitOfWork.Recipes.IsNameMustNotExist(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());

            unitOfWork.Recipes.Create(new Recipe() { Id = idRecipe, Name = nameRecipe, IdCategory = idCategory });
            unitOfWork.SaveDataTable("Recipes.json", JsonConvert.SerializeObject(unitOfWork.Recipes.GetAll()));
        }
        public void Delete(int idRecipe)
        {
            Console.Clear();
            Console.Write("Are you sure you want to delete the recipe? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                int idCategory = unitOfWork.Recipes.Get(idRecipe).IdCategory;
                foreach (var a in unitOfWork.AmountIngredients.GetAll().ToList().Where(x => x.IdRecipe == idRecipe))
                    unitOfWork.AmountIngredients.Delete(a.Id);

                foreach (var a in unitOfWork.CookingSteps.GetAll().ToList().Where(x => x.IdRecipe == idRecipe))
                    unitOfWork.CookingSteps.Delete(a.Id);

                unitOfWork.Recipes.Delete(idRecipe);
                unitOfWork.SaveChangesRecipe();
            }
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
            if (level <= levelLimitation)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
                foreach (var recipe in unitOfWork.Recipes.GetAll().Where(x => x.IdCategory == thisEntity.Id))
                {
                    items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.IdCategory, TypeEntity = "recipe" });
                }
                foreach (var child in unitOfWork.Categories.GetAll().FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
                {
                    var entityMenu = new EntityMenu() { Id = child.Id, Name = child.Name, ParentId = child.ParentId };
                    BuildCurrentOpenRecipesCategories(items, entityMenu, level + 1, levelLimitation);
                }
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
