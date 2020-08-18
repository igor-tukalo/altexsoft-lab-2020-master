using System;
using System.Collections.Generic;
using System.Linq;
using task2.Controls;
using task2.Models;

namespace task2.Instruments
{
    public abstract class MenuNavigation
    {
        protected EntityMenu CategoryRecipe { get; set; } // used to return to the recipe categories menu
        protected JsonControl jsonControl { get; set; }
        /// <summary>
        /// List of menu items
        /// </summary>
        protected List<EntityMenu> ItemsMenu = new List<EntityMenu>();

        /// <summary>
        /// The method calls the logic of the specified menu
        /// </summary>
        /// <param name="id"></param>
        protected virtual void SelectMethodMenu(int id) 
        { 
        
        }

        /// <summary>
        /// Call the navigation menu with a populated menu list and logic
        /// </summary>
        protected void CallMenuNavigation()
        {
            Navigation menu = new Navigation(ItemsMenu, SelectMethodMenu);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="thisEntity">item which have ParentId 0</param>
        /// <param name="level">level hierarchy</param>
        protected void BuildHierarchicalMenu(List<EntityMenu> items, EntityMenu thisEntity, int level)
        {
            ItemsMenu.Add(new Category(thisEntity.Id,name: $"{new string('-', level)}{thisEntity.Name}", thisEntity.ParentId));
            foreach (var child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Id))
            {
                BuildHierarchicalMenu(items, child, level + 1);
            }
        }

        /// <summary>
        /// Build a hierarchical menu to the specified hierarchy level
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="thisEntity">item which have ParentId 0</param>
        /// <param name="level">level hierarchy</param>
        /// <param name="levelLimitation">level limitation hierarchy</param>
        protected void BuildHierarchicalMenu(List<EntityMenu> items, EntityMenu thisEntity, int level, int levelLimitation)
        {
            if (level <= levelLimitation)
            {
                ItemsMenu.Add(new Category(thisEntity.Id, name: $"{new string('-', level)}{thisEntity.Name}", thisEntity.ParentId));

            }     
            foreach (var child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Id))
            {
                BuildHierarchicalMenu(items, child, level + 1, levelLimitation);
            }
        }

        /// <summary>
        /// Build a hierarchical menu to the specified hierarchy level
        /// </summary>
        /// <param name="items">list items which have a parent id</param>
        /// <param name="thisEntity">item which have ParentId 0</param>
        /// <param name="level">level hierarchy</param>
        /// <param name="levelLimitation">level limitation hierarchy</param>
        protected void BuildHierarchicalMenu(List<EntityMenu> items, List<Recipe> recipes, EntityMenu thisEntity, int level, int levelLimitation)
        {
            if (level <= levelLimitation)
            {
                ItemsMenu.Add(new Category(thisEntity.Id, name: $"{new string('-', level)}{thisEntity.Name}", thisEntity.ParentId));
                foreach (var recipe in recipes.Where(x => x.IdCategory == thisEntity.Id))
                {
                    ItemsMenu.Add(new Category(recipe.Id, name: $"  {recipe.Name}", thisEntity.ParentId, "Recipe"));
                }
            }
            foreach (var child in items.FindAll((x) => x.ParentId == thisEntity.Id).OrderBy(x => x.Name))
            {
                BuildHierarchicalMenu(items, child, level + 1, levelLimitation);
            }
        }

        /// <summary>
        /// Method for getting the menu list
        /// </summary>
        /// <param name="IdMenu">id of the menu from which the hierarchy starts</param>
        public virtual void GetMenuItems(int IdMenu = 1) { }

        protected virtual void AddMenuItem(string filePath) { }

    }
}
