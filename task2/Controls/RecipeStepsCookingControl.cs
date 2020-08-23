using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class RecipeStepsCookingControl : MenuNavigation
    {
        public RecipeStepsCookingControl(UnitOfWork _unitOfWork, int recipeId)
        {
            unitOfWork = _unitOfWork;
            RecipeId = recipeId;

            ItemsMenuMain = new List<EntityMenu>
                {
                    new StepCooking(name: "  Add step cooking"),
                    new StepCooking(name: "  Save the cooking steps and return to create the recipe"),
                    new StepCooking(name: "  Clear the cooking steps and return to create the recipe")
                };
        }

        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();

            ItemsMenu = new List<EntityMenu>(ItemsMenuMain);

            foreach (var s in unitOfWork.StepsCooking.GetAll().Where(x => x.IdRecipe == RecipeId).OrderBy(x => x.Step))
            {
                ItemsMenu.Add(new StepCooking(id: s.Id, name: $"    {s.Step}. {s.Name}", typeEntity: "step", parentId: RecipeId));
            }
            Console.WriteLine("\n To change cooking steps, select a cooking step and press enter. \n");

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
                        GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        // Save the cooking steps and return to create the recipe
                        new RecipeСreateControl(RecipeId, unitOfWork.Recipes.Get(RecipeId).IdCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        // Cancel and return to create the recipe
                        foreach (var a in unitOfWork.StepsCooking.GetAll().ToList().Where(x => x.IdRecipe == RecipeId))
                            unitOfWork.StepsCooking.Delete(a.Id);

                        new RecipeСreateControl(RecipeId, unitOfWork.Recipes.Get(RecipeId).IdCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                default:
                    {
                        // Edit step cooking
                        if (ItemsMenu[id].TypeEntity == "step")
                            new ContextMenuStepsCooking(_unitOfWork: unitOfWork, idMenuNavigation: ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        void AddStepCooking()
        {
            Console.Clear();
            int idStep = unitOfWork.StepsCooking.GetAll().Count() > 0 ? unitOfWork.StepsCooking.GetAll().Max(x => x.Id) + 1 : 1;
            int CurrentStep = unitOfWork.StepsCooking.GetAll().Where(x => x.IdRecipe == RecipeId).Count() > 0 ?
                unitOfWork.StepsCooking.GetAll().Where(x=>x.IdRecipe== RecipeId).Max(x => x.Step) + 1 : 1;
            Console.Write($" Describe the cooking step {CurrentStep}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            unitOfWork.StepsCooking.Create(new StepCooking() { Id = idStep, Step = CurrentStep, Name = stepName, IdRecipe = RecipeId });

            Console.Write(" Add another cooking step? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                AddStepCooking();
            }
            else new RecipeStepsCookingControl(unitOfWork, RecipeId);
        }
    }
}
