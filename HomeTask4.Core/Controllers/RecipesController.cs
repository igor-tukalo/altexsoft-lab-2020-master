using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HomeTask4.Core.Controllers
{
    public class RecipesController : BaseController, IRecipesControl
    {
        private List<AmountIngredient> AmountIngredients => UnitOfWork.Repository.GetListAsync<AmountIngredient>().Result;
        private List<Ingredient> Ingredients => UnitOfWork.Repository.GetListAsync<Ingredient>().Result;
        private List<CookingStep> CookingSteps => UnitOfWork.Repository.GetListAsync<CookingStep>().Result;
        private List<Recipe> Recipes => UnitOfWork.Repository.GetListAsync<Recipe>().Result;
        private List<Category> Categories => UnitOfWork.Repository.GetListAsync<Category>().Result;
        public RecipesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public void View(int idRecipe)
        {
            Recipe recipe = UnitOfWork.Repository.GetByIdAsync<Recipe>(idRecipe).Result;
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {ValidManager.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            //ingredients recipe
            foreach (AmountIngredient a in AmountIngredients.Where(x => x.RecipeId == recipe.Id))
            {
                foreach (Ingredient i in Ingredients.Where(x => x.Id == a.IngredientId))
                {
                    Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                }
            }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (CookingStep s in CookingSteps.Where(x => x.RecipeId == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {ValidManager.WrapText(10, s.Name, "\n       ")}");
            }
        }

        public void Edit(int id)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = IsNameMustNotExist(ValidManager.NullOrEmptyText(Console.ReadLine()));
            Recipe recipe = UnitOfWork.Repository.GetByIdAsync<Recipe>(id).Result;
            recipe.Name = nameRecipe;
            UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public void ChangeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = ValidManager.NullOrEmptyText(Console.ReadLine());
            Recipe recipe = UnitOfWork.Repository.GetByIdAsync<Recipe>(idRecipe).Result;
            recipe.Description = description;
            UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public void Add(int idCategory)
        {
            Console.WriteLine($"\n    The recipe will be added to the category: {UnitOfWork.Repository.GetByIdAsync<Category>(idCategory).Result.Name}");
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = IsNameMustNotExist(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = ValidManager.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.Repository.AddAsync(new Recipe() { Name = nameRecipe, Description = description, CategoryId = idCategory });
        }

        public void Delete(int id)
        {
            Console.Clear();
            Console.Write("Are you sure you want to delete the recipe? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            UnitOfWork.Repository.DeleteAsync(UnitOfWork.Repository.GetByIdAsync<Recipe>(id).Result);
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
                foreach (Recipe recipe in Recipes.Where(x => x.CategoryId == thisEntity.Id))
                {
                    items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.CategoryId, TypeEntity = "recipe" });
                }
            }
            foreach (Category child in Categories.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
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

        private string IsNameMustNotExist(string name)
        {
            while (Recipes.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = ValidManager.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
