using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class RecipesControl : BaseControl, IRecipesControl
    {
        public int GetIdCategory(int idRecipe)
        {
            return DBControl.Recipes.Get(idRecipe).IdCategory;
        }
        public void View(int idRecipe)
        {
            var recipe = DBControl.Recipes.Get(idRecipe);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {Validation.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            //ingredients recipe
                foreach (var a in DBControl.AmountIngredients.Items.Where(x => x.IdRecipe == recipe.Id))
                {
                    foreach (var i in DBControl.Ingredients.Items.Where(x => x.Id == a.IdIngredient))
                    {
                        Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                    }
                }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (var s in DBControl.CookingSteps.Items.Where(x => x.IdRecipe == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {Validation.WrapText(10, s.Name, "\n       ")}");
            }
        }

        public override void Edit(int id)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = DBControl.Recipes.IsNameMustNotExist(Console.ReadLine());
            var recipe = DBControl.Recipes.Get(id);
            recipe.Name = nameRecipe;
            DBControl.Recipes.Update(recipe);
            base.Edit(id);
        }

        public void ChangeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            var recipe = DBControl.Recipes.Get(idRecipe);
            recipe.Description = description;
            DBControl.Recipes.Update(recipe);
            DBControl.SaveAllData();
        }

        public void Add(int idCategory)
        {
            int idRecipe = DBControl.Recipes.Items.Count() > 0 ? DBControl.Recipes.Items.Max(x => x.Id) + 1 : 1;
            Console.WriteLine($"\n    The recipe will be added to the category: {DBControl.Categories.Get(idCategory).Name}");
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = DBControl.Recipes.IsNameMustNotExist(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            DBControl.Recipes.Create(new Recipe() { Id = idRecipe, Name = nameRecipe, Description=description, IdCategory = idCategory });
            base.Add();
        }

        public override void Delete(int id)
        {
            Console.Clear();
            Console.Write("Are you sure you want to delete the recipe? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            DBControl.AmountIngredients.Items.RemoveAll(r => r.IdRecipe == id);
            DBControl.CookingSteps.Items.RemoveAll(r => r.IdRecipe == id);
            DBControl.Recipes.Delete(id);
            base.Delete(id);
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
            foreach (var recipe in DBControl.Recipes.Items.Where(x => x.IdCategory == thisEntity.Id))
            {
                items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.IdCategory, TypeEntity = "recipe" });
            }
            foreach (var child in DBControl.Categories.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
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
