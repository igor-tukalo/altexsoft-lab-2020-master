using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeAddStepsCookingControl : RecipeViewControl
    {
        Recipe AddedRecipe { get; set; }
        int CurrentStep { get; set; }
        public RecipeAddStepsCookingControl()
        { 
        }

        public void GetMenuItems(EntityMenu categoryRecipeMenu, Recipe addedRecipe, List<AmountRecipeIngredient> addedAmountIngredients, int currentStep=0)
        {
            CurrentStep = currentStep;
            AddedRecipe = addedRecipe;
            AmountRecipeIngredients = addedAmountIngredients;
            CategoryRecipe = categoryRecipeMenu;

            Console.Clear();

            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "    Add step cooking"),
                new Category(name: "    Create recipe"),
                new Category(name: "    Cancel create recipe")
            };

            foreach (var s in StepCookings.Where(x=>x.IdRecipe==addedRecipe.Id).OrderBy(x => x.Step))
            {
                ItemsMenu.Add(new StepCooking(id: s.Id, name: $"    {s.Step}. {s.Name}",typeEntity:"step"));
            }
            Console.WriteLine("\n To change cooking steps, select a cooking step and press enter. \n");
            RecipeView(AddedRecipe);
            CallMenuNavigation();

        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Add step cooking
                        AddStepCooking();
                    }
                    break;
                case 1:
                    {
                        // Create recipe
                        if (StepCookings.Count>0)
                        {
                            Recipes.Add(AddedRecipe);

                            Validation.SaveSelectedDataJson(Recipes, AmountRecipeIngredients, StepCookings);

                            NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                            navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
                        }
                    }
                    break;
                case 2:
                    {
                        // Cancel create recipe
                        NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                        navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
                    }
                    break;
                default:
                    {
                        // Edit step cooking
                        if(ItemsMenu[id].TypeEntity=="step")
                        EditStepCooking(ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        private void AddStepCooking()
        {
            Console.Clear();
            int idStep = StepCookings.Max(x => x.Id) + 1;
            CurrentStep++;
            Console.Write($" Describe the cooking step {CurrentStep}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            StepCookings.Add(new StepCooking() { Id = idStep, Step = CurrentStep, Name = stepName, IdRecipe= AddedRecipe.Id });

            //ViewAddedStepsCooKing();

            Console.Write(" Add another cooking step? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            { 
                AddStepCooking(); 
            }
            else GetMenuItems(CategoryRecipe, AddedRecipe, AmountRecipeIngredients,CurrentStep);
        }

        private void EditStepCooking(int idStep)
        {
            Console.Clear();
            ConsoleKey consoleKey;
            consoleKey = Validation.EdirOrDeleteorCancel();
            var step = (from s in StepCookings
                            where s.Id == idStep
                        select s).First();

            if (consoleKey == ConsoleKey.D1)
            {
                // edit
                Console.WriteLine($" You are editing {step.Step}. {step.Name}");
                Console.Write($" Describe the new cooking step: ");
                step.Name = Validation.NullOrEmptyText(Console.ReadLine());
                GetMenuItems(CategoryRecipe, AddedRecipe, AmountRecipeIngredients, CurrentStep);
            }
            else if (consoleKey == ConsoleKey.D2)
            {
                // remove
                foreach (var s in StepCookings.Where(x => x.Step > step.Step))
                {
                    s.Step--;
                }
                StepCookings.Remove(step);
                GetMenuItems(CategoryRecipe, AddedRecipe, AmountRecipeIngredients, CurrentStep);
            }
            else
            {
                //cancel
                GetMenuItems(CategoryRecipe, AddedRecipe, AmountRecipeIngredients, CurrentStep);
            }
        }
    }
}
