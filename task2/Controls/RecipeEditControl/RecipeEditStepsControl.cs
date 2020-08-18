using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeEditStepsControl : RecipeAddStepsCookingControl
    {
        public RecipeEditStepsControl()
        {
            ItemsMenuMain = new List<EntityMenu>
            {
                new Category(name: "    Add step cooking"),
                new Category(name: "    Save step cooking changes"),
                new Category(name: "    Cancel")
            };
        }

        protected override void SaveDataRecipe()
        {
            Validation.SaveSelectedDataJson(stepCookings: StepCookings);
            ReturnPreviousMenu();
        }

        protected override void CancelCreateRecipe()
        {
            var recipe = (from r in Recipes
                          where r.Id == RecipeViewSelected.Id
                          select r).First();

            RecipeEditControl recipeEditControl = new RecipeEditControl();
            recipeEditControl.GetMenuItems(CategoryRecipe, recipe);
        }

        public override void ReturnPreviousMenu()
        {
            GetMenuItems(CategoryRecipe, RecipeViewSelected,currentStep: CurrentStep);
        }
    }
}
