using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeEditCategoryControl : CategoryControl
    {
        Recipe EditableRecipe { get; set; }
        List<Recipe> Recipes { get; set; }
        public RecipeEditCategoryControl()
        {
            Recipes = JsonConvert.DeserializeObject<List<Recipe>>(new JsonControl("Recipes.json").GetJsonData());
        }

        public void GetMenuItems(EntityMenu categoryRecipe, Recipe editableRecipe, int IdMenu = 1)
        {
            CategoryRecipe = categoryRecipe;
            EditableRecipe = editableRecipe;

            Console.Clear();

            ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Cancel"),
                };

            var parent = CategoriesList.Find((x) => x.Id == IdMenu);
            BuildHierarchicalMenu(new List<EntityMenu>(CategoriesList), parent, 1);

            Console.WriteLine("\n    Select a recipe category and press enter.\n");
            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0: 
                    {
                        // Cancel
                        RecipeEditControl recipeEditControl = new RecipeEditControl();
                        recipeEditControl.GetMenuItems(CategoryRecipe, EditableRecipe);
                    }
                    break;
                default:
                    {
                        // Change category recipe
                        if (id > 0)
                        {
                            Recipes = Recipes
                            .Select(r => r.Id == EditableRecipe.Id
                            ? new Recipe {Id= r.Id, Name = r.Name, Description = r.Description, IdCategory = ItemsMenu[id].Id }
                            : r).ToList();

                            Validation.SaveSelectedDataJson(recipes: Recipes);

                            var recipe = (from r in Recipes
                                          where r.Id == EditableRecipe.Id
                                          select r).First();

                            RecipeEditControl recipeEditControl = new RecipeEditControl();
                            recipeEditControl.GetMenuItems(CategoryRecipe, recipe);
                        }
                    }
                    break;
            }
        }
    }
}
