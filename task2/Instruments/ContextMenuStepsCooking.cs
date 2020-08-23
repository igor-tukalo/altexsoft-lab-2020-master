using System;
using System.Linq;
using task2.Controls;
using task2.Models;
using task2.Repositories;

namespace task2.Instruments
{
    public class ContextMenuStepsCooking : ContextMenuNavigation
    {
        public ContextMenuStepsCooking(UnitOfWork _unitOfWork, int idMenuNavigation, int recipeId = 0) : base(_unitOfWork, idMenuNavigation, recipeId)
        {
        }

        protected override void Rename()
        {
            Console.Write("  Enter a new description for the cooking step: ");
            string newName = Console.ReadLine();

            unitOfWork.StepsCooking.Update(new StepCooking
            {
                Id = unitOfWork.StepsCooking.Get(IdMenuNavigation).Id,
                Name = newName,
                Step = unitOfWork.StepsCooking.Get(IdMenuNavigation).Step,
                IdRecipe = unitOfWork.StepsCooking.Get(IdMenuNavigation).IdRecipe
            });

            Cancel();
        }

        protected override void Delete()
        {
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                int idRecipe = unitOfWork.StepsCooking.Get(IdMenuNavigation).IdRecipe;

                foreach (var s in unitOfWork.StepsCooking.GetAll().Where(x => x.IdRecipe == idRecipe && x.Step > unitOfWork.StepsCooking.Get(IdMenuNavigation).Step))
                {
                    s.Step--;
                }

                unitOfWork.StepsCooking.Delete(IdMenuNavigation);

                new RecipeStepsCookingControl(unitOfWork, idRecipe).GetMenuItems();
            }
            else Cancel();
        }

        protected override void Cancel()
        {
            new RecipeStepsCookingControl(unitOfWork, unitOfWork.StepsCooking.Get(IdMenuNavigation).IdRecipe).GetMenuItems();
        }
    }
}
