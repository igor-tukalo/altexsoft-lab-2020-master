using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeEditControl : RecipeViewControl
    {

        Recipe EditableRecipe { get; set; }
        override public void GetMenuItems(EntityMenu categoryRecipe, Recipe editableRecipe)
        {
            CategoryRecipe = categoryRecipe;
            EditableRecipe = editableRecipe;

            Console.Clear();

            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "    Edit recipe name"),
                new Category(name: "    Edit description recipe"),
                new Category(name: "    Change category recipe"),
                new Category(name: "    Edit recipe ingredients"),
                new Category(name: "    Edit cooking steps"),
                new Category(name: "    Cancel")

            };
            var recipe = (from r in Recipes
                          where r.Id == EditableRecipe.Id
                          select r).First();

            Console.WriteLine("\n    Edit recipe\n");
            RecipeView(recipe);
            CallMenuNavigation();

        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Edit recipe name
                        Console.Clear();
                        Console.WriteLine($"Current recipe name: {EditableRecipe.Name}");
                        Console.Write("Do you really want to change the name of the recipe? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            Console.Write("Enter a new name for the recipe: ");
                            string recipeNewName = Validation.IsExsistsNameList(new List<EntityMenu>(Recipes), Validation.NullOrEmptyText(Console.ReadLine()));

                            Recipes = Recipes
                            .Select(r => r.Id == EditableRecipe.Id
                            ? new Recipe { Id = r.Id, Name = recipeNewName, Description = r.Description, IdCategory = r.IdCategory }
                            : r).ToList();

                            Validation.SaveSelectedDataJson(recipes: Recipes);
                            GetMenuItems(CategoryRecipe, EditableRecipe);
                        }
                        else GetMenuItems(CategoryRecipe, EditableRecipe);
                    }
                    break;
                case 1:
                    {
                        // Edit description recipe
                        Console.Clear();
                        Console.WriteLine($"Current description: {EditableRecipe.Description}");
                        Console.Write($"Are you sure you want to change the recipe description? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            Console.Write("Enter a new description: ");
                            string newDescRecipe = Validation.NullOrEmptyText(Console.ReadLine());

                            Recipes = Recipes
                            .Select(r => r.Id == EditableRecipe.Id
                            ? new Recipe { Id = r.Id, Name = r.Name, Description = newDescRecipe, IdCategory = r.IdCategory }
                            : r).ToList();

                            Validation.SaveSelectedDataJson(recipes: Recipes);
                            GetMenuItems(CategoryRecipe, EditableRecipe);
                        }
                        else GetMenuItems(CategoryRecipe, EditableRecipe);
                    }
                    break;
                case 2:
                    {
                        // Change category recipe
                        RecipeEditCategoryControl recipeEditCategoryControl = new RecipeEditCategoryControl();
                        recipeEditCategoryControl.GetMenuItems(CategoryRecipe, EditableRecipe);
                    }
                    break;
                case 3:
                    {
                        // Edit recipe ingredients

                    }
                    break;
                case 4:
                    {
                        // Edit cooking steps

                    }
                    break;
                case 5:
                    {
                        // Cancel
                        var recipe = (from r in Recipes
                                      where r.Id == EditableRecipe.Id
                                      select r).First();

                        RecipeViewControl recipeViewControl = new RecipeViewControl();
                        recipeViewControl.GetMenuItems(CategoryRecipe, recipe);
                    }
                    break;
            }
        }
    }
}
