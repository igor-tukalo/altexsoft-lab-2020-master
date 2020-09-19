using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.CRUD
{
    public class RecipesControl : BaseCategoriesRecipesControl, IRecipesControl
    {
        private readonly IngredientRepository ingredientRepository;
        public RecipesControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (unitOfWork != null)
            {
                ingredientRepository = unitOfWork.Ingredients;
            }
        }

        public int GetIdCategory(int idRecipe)
        {
            return RecipeRepository.GetItem(idRecipe).CategoryId;
        }
        public void View(int idRecipe)
        {
            Recipe recipe = RecipeRepository.GetItem(idRecipe);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {ValidManager.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            //ingredients recipe
            foreach (AmountIngredient a in AmountIngredientRepository.Items.Where(x => x.RecipeId == recipe.Id))
            {
                foreach (Ingredient i in ingredientRepository.Items.Where(x => x.Id == a.IngredientId))
                {
                    Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                }
            }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (CookingStep s in CookingStepRepository.Items.Where(x => x.RecipeId == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {ValidManager.WrapText(10, s.Name, "\n       ")}");
            }
        }

        public void Edit(int id)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = RecipeRepository.IsNameMustNotExist(ValidManager.NullOrEmptyText(Console.ReadLine()));
            Recipe recipe = RecipeRepository.GetItem(id);
            recipe.Name = nameRecipe;
            RecipeRepository.Update(recipe);
        }

        public void ChangeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = ValidManager.NullOrEmptyText(Console.ReadLine());
            Recipe recipe = RecipeRepository.GetItem(idRecipe);
            recipe.Description = description;
            RecipeRepository.Update(recipe);
        }

        public void Add(int idCategory)
        {
            Console.WriteLine($"\n    The recipe will be added to the category: {CategoryRepository.GetItem(idCategory).Name}");
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = RecipeRepository.IsNameMustNotExist(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = ValidManager.NullOrEmptyText(Console.ReadLine());
            RecipeRepository.Create(new Recipe() { Name = nameRecipe, Description = description, CategoryId = idCategory });
        }

        public void Delete(int id)
        {
            Console.Clear();
            Console.Write("Are you sure you want to delete the recipe? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            RecipeRepository.Delete(RecipeRepository.GetItem(id));
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
            {
                return;
            }
            if (items != null && thisEntity != null)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
                foreach (Recipe recipe in RecipeRepository.Items.Where(x => x.CategoryId == thisEntity.Id))
                {
                    items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.CategoryId, TypeEntity = "recipe" });
                }
            }
            foreach (Category child in CategoryRepository.Items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                EntityMenu entityMenu = new EntityMenu() { Id = child.Id, Name = child.Name, ParentId = child.ParentId };
                if (items != null)
                {
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
                foreach (EntityMenu child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Id))
                {
                    BuildCurrentOpenRecipesCategories(items, child, level + 1, levelLimitation);
                }
            }
        }
    }
}
