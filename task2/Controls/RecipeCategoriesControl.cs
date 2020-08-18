using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Controls;
using task2.Controls.RecipeAddConrols;
using task2.Instruments;
using task2.Models;

namespace task2
{
    public class RecipeCategoriesControl : CategoryControl
    {
        JsonControl JsonControlRecipes { get; set; }
        List<Recipe> RecipesList { get; set; }
        int IdPrevCategory { get; set; }
        public RecipeCategoriesControl()
        {
            JsonControlRecipes = new JsonControl("Recipes.json");
            jsonControl = new JsonControl("Categories.json");

            //De - serialize to object or create new list
            CategoriesList = JsonConvert.DeserializeObject<List<Category>>(jsonControl.GetJsonData());
            RecipesList = JsonConvert.DeserializeObject<List<Recipe>>(JsonControlRecipes.GetJsonData());
        }

        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(jsonControl.JsonFileName))
            {

                ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Add recipe"),
                    new Category(name: "    Return to main menu")
                };

                var parent = CategoriesList.Find((x) => x.Id == IdMenu);
                IdPrevCategory = parent.Id;
                BuildHierarchicalMenu(new List<EntityMenu>(CategoriesList), RecipesList, parent, 1, 2);

            }
            else Console.WriteLine(" There are no recipe categories! Add categories!");

            Console.WriteLine(" Recipe categories");
            Console.WriteLine(" To return to the previous category, click on the topmost category.");
            Console.WriteLine(" Recipes are inside categories and do not have a symbol in the name -");

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {

                        RecipeAddControl recipeAddControl = new RecipeAddControl();
                        recipeAddControl.AddRecipe(GetCategory());

                    }
                    break;
                case 1:
                    {
                        MainMenuControl mainMenuControl = new MainMenuControl();
                        mainMenuControl.GetMenuItems();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].TypeEntity == "Recipe")
                        {

                            var recipe = (from r in RecipesList
                                          where r.Id == ItemsMenu[id].Id
                                          select r).First();

                            RecipeViewControl RecipeView = new RecipeViewControl();
                            RecipeView.GetMenuItems(GetCategory(), recipe);
                        }
                        else
                        {
                            if (ItemsMenu[id].ParentId != 0)
                            {
                                NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                                navigateRecipeCategories.GetRecipesCategory(ItemsMenu[id], IdPrevCategory);
                            }

                        }
                    }
                    break;
            }
        }

        private EntityMenu GetCategory()
        {
            return (from t in ItemsMenu
                    where t.Id == IdPrevCategory
                    select t).First();
        }
    }
}
