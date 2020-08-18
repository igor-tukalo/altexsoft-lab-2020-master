﻿using Newtonsoft.Json;
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
        override public void GetMenuItems(EntityMenu categoryRecipe, Recipe editableRecipe)
        {
            CategoryRecipe = categoryRecipe;
            RecipeViewSelected = editableRecipe;

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
                          where r.Id == RecipeViewSelected.Id
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
                        Console.WriteLine($"Current recipe name: {RecipeViewSelected.Name}");
                        Console.Write("Do you really want to change the name of the recipe? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            Console.Write("Enter a new name for the recipe: ");
                            string recipeNewName = Validation.IsExsistsNameList(new List<EntityMenu>(Recipes), Validation.NullOrEmptyText(Console.ReadLine()));

                            Recipes = Recipes
                            .Select(r => r.Id == RecipeViewSelected.Id
                            ? new Recipe { Id = r.Id, Name = recipeNewName, Description = r.Description, IdCategory = r.IdCategory }
                            : r).ToList();

                            Validation.SaveSelectedDataJson(recipes: Recipes);
                            GetMenuItems(CategoryRecipe, RecipeViewSelected);
                        }
                        else GetMenuItems(CategoryRecipe, RecipeViewSelected);
                    }
                    break;
                case 1:
                    {
                        // Edit description recipe
                        Console.Clear();
                        Console.WriteLine($"Current description: {RecipeViewSelected.Description}");
                        Console.Write($"Are you sure you want to change the recipe description? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            Console.Write("Enter a new description: ");
                            string newDescRecipe = Validation.NullOrEmptyText(Console.ReadLine());

                            Recipes = Recipes
                            .Select(r => r.Id == RecipeViewSelected.Id
                            ? new Recipe { Id = r.Id, Name = r.Name, Description = newDescRecipe, IdCategory = r.IdCategory }
                            : r).ToList();

                            Validation.SaveSelectedDataJson(recipes: Recipes);
                            GetMenuItems(CategoryRecipe, RecipeViewSelected);
                        }
                        else GetMenuItems(CategoryRecipe, RecipeViewSelected);
                    }
                    break;
                case 2:
                    {
                        // Change category recipe
                        RecipeEditCategoryControl recipeEditCategoryControl = new RecipeEditCategoryControl();
                        recipeEditCategoryControl.GetMenuItems(CategoryRecipe, RecipeViewSelected);
                    }
                    break;
                case 3:
                    {
                        ItemsMenu = new List<EntityMenu>
                        {
                            new Category(name: "    Remove previously added ingredient"),
                            new Category(name: "    Cancel")
                        };
                        // Edit recipe ingredients
                        RecipeEditIngredientsControl recipeEditIngredientsControl = new RecipeEditIngredientsControl();
                        recipeEditIngredientsControl.GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe);
                    }
                    break;
                case 4:
                    {
                        // Edit cooking steps
                        RecipeEditStepsControl recipeEditStepsControl = new RecipeEditStepsControl();
                        recipeEditStepsControl.GetMenuItems(CategoryRecipe, RecipeViewSelected,currentStep:StepCookings.Where(x=>x.IdRecipe== RecipeViewSelected.Id).Max(x=>x.Step));
                    }
                    break;
                case 5:
                    {
                        // Cancel
                        var recipe = (from r in Recipes
                                      where r.Id == RecipeViewSelected.Id
                                      select r).First();

                        RecipeViewControl recipeViewControl = new RecipeViewControl();
                        recipeViewControl.GetMenuItems(CategoryRecipe, recipe);
                    }
                    break;
            }
        }
    }
}
