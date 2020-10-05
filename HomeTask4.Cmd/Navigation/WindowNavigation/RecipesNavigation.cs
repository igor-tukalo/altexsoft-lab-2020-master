using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class RecipesNavigation : NavigationManager, IRecipesNavigation
    {
        private int _currentCategoryId = 1;
        private int _prevCategoryId;
        private List<EntityMenu> _itemsMenu;
        private readonly IRecipesController _recipesController;
        private readonly IRecipesContextMenuNavigation _recipesContextMenuNavigation;

        public RecipesNavigation(IValidationNavigation validationNavigation,
            IRecipesController recipesController,
            IRecipesContextMenuNavigation recipesContextMenuNavigation) : base(validationNavigation)
        {
            _recipesController = recipesController;
            _recipesContextMenuNavigation = recipesContextMenuNavigation;
        }

        #region private methods
        /// <summary>
        ///  Get recipes categories
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="category">parent element</param>
        /// <param name="level">level hierarchy</param>
        /// <param name="levelLimitation">level limitation hierarchy</param>
        private async Task BuildRecipesCategoriesAsync(List<EntityMenu> items, Category category, int level, int levelLimitation)
        {
            if (level > levelLimitation)
            {
                return;
            }
            if (items != null && category != null)
            {
                items.Add(new EntityMenu() { Id = category.Id, Name = $"{new string('-', level)}{category.Name}", ParentId = category.ParentId });
                List<Recipe> recipes = await _recipesController.GetRecipessWhereCategoryIdAsync(category.Id);
                foreach (Recipe recipe in recipes)
                {
                    items.Add(new EntityMenu() { Id = recipe.Id, Name = $"  {recipe.Name}", ParentId = recipe.CategoryId, TypeEntity = "recipe" });
                }
            }
            List<Category> categories = await _recipesController.GetCategoriesWhereParentIdAsync(category.Id);
            foreach (Category child in categories.OrderBy(x => x.Name))
            {
                EntityMenu entityMenu = new EntityMenu() { Id = child.Id, Name = child.Name, ParentId = child.ParentId };
                if (items != null)
                {
                    await BuildCurrentOpenRecipesCategoryAsync(items, entityMenu, level + 1, levelLimitation);
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
        private async Task BuildCurrentOpenRecipesCategoryAsync(List<EntityMenu> items, EntityMenu thisEntity, int level, int levelLimitation)
        {
            if (level <= levelLimitation)
            {
                items.Add(new EntityMenu() { Id = thisEntity.Id, Name = $"{new string('-', level)}{thisEntity.Name}", ParentId = thisEntity.ParentId });
                foreach (EntityMenu child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Id))
                {
                    await BuildCurrentOpenRecipesCategoryAsync(items, child, level + 1, levelLimitation);
                }
            }
        }

        private async Task MovementCategoriesRecipesAsync(int nextCategoryid)
        {
            // forward movement. one levels below
            if (_currentCategoryId != nextCategoryid)
            {
                _currentCategoryId = nextCategoryid;
            }
            // backward movement. one level up
            else
            {
                _currentCategoryId = _prevCategoryId == 0 ? 1 : _prevCategoryId;
            }
            await ShowMenuAsync();
        }

        private async Task AddRecipeAsync()
        {
            Console.WriteLine($"\n    The recipe will be added to the category: {(await _recipesController.GetCategoryByIdAsync(_currentCategoryId)).Name}");
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            Console.Write("\n    Enter recipe description: ");
            string description = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _recipesController.AddAsync(nameRecipe, description, _currentCategoryId);
            await ShowMenuAsync();
        }
        #endregion

        public async Task ShowMenuAsync()
        {
            Console.Clear();
            _itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add recipe" },
                    new EntityMenu(){ Name = "    Return to main menu" }
                };
            Category parent = await _recipesController.GetCategoryByIdAsync(_currentCategoryId);
            _prevCategoryId = parent.ParentId;
            await BuildRecipesCategoriesAsync(_itemsMenu, parent, 1, 2);
            await CallNavigationAsync(_itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddRecipeAsync();
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                default:
                    {
                        if (_itemsMenu[id].TypeEntity == "recipe")
                        {
                            await _recipesContextMenuNavigation.ShowMenuAsync(_itemsMenu[id].Id);
                            await ShowMenuAsync();
                        }
                        else
                        {
                            await MovementCategoriesRecipesAsync(_itemsMenu[id].Id);
                        }
                    }
                    break;
            }
        }
    }
}
