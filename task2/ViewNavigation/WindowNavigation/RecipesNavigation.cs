using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class RecipesNavigation : BaseNavigation, INavigation
    {
        readonly IRecipesControl Recipes;
        readonly ICategoriesControl Categories;
        int IdNextCategory = 1;
        int IdPrevCategory;

        public RecipesNavigation(ICategoriesControl categories, IRecipesControl recipes)
        {
            Categories = categories;
            Recipes = recipes;
        }
        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add recipe" },
                    new EntityMenu(){ Name = "    Return to main menu" }
                };
            var parent = Categories.GetParentCategory(IdNextCategory);
            IdPrevCategory = parent.ParentId;
            Recipes.BuildRecipesCategories(ItemsMenu, parent, 1, 2);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Recipes.Add(IdNextCategory);
                        MovementCategoriesRecipes(IdNextCategory);
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new MainWindowNavigation()).CallMenu();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].TypeEntity == "recipe")
                            new ProgramMenu(new RecipesContextMenuNavigation(ItemsMenu[id].Id, new RecipesControl())).CallMenu();
                        else
                            MovementCategoriesRecipes(ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        public void MovementCategoriesRecipes(int idnextCategory)
        {
            // forward movement. one levels below
            if (IdNextCategory != idnextCategory)
                IdNextCategory = idnextCategory;
            // backward movement. one level up
            else
                IdNextCategory = IdPrevCategory == 0 ? 1 : IdPrevCategory;
            CallNavigation();
        }
    }
}
