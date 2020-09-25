using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.Controllers
{
    public class CookingStepsController : BaseController, ICookingStepsController
    {
        private List<CookingStep> CookingSteps => UnitOfWork.Repository.GetList<CookingStep>();

        public CookingStepsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<EntityMenu> GetItems(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (CookingSteps != null)
            {
                foreach (CookingStep s in CookingSteps.Where(x => x.RecipeId == idRecipe))
                {
                    if (itemsMenu != null)
                    {
                        itemsMenu.Add(new EntityMenu() { Id = s.Id, Name = $"    {s.Step}. {s.Name}", ParentId = s.RecipeId });
                    }
                }
            }
            return itemsMenu;
        }

        public void Add(int idRecipe)
        {
            int CurrentStep = CookingSteps.Where(x => x.RecipeId == idRecipe).Any() ?
                CookingSteps.Where(x => x.RecipeId == idRecipe).Max(x => x.Step) + 1 : 1;
            Console.Write($"\n    Describe the cooking step {CurrentStep}: ");
            string stepName = ValidManager.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.Repository.Add(new CookingStep() { Step = CurrentStep, Name = stepName, RecipeId = idRecipe });
            Console.Write("\n    Add another cooking step? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            Add(idRecipe);
        }

        public void Edit(int id)
        {
            CookingStep cookingStep = UnitOfWork.Repository.GetById<CookingStep>(id);
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = ValidManager.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            UnitOfWork.Repository.Update(cookingStep);
        }

        public void Delete(int id, int idRecipe)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            foreach (CookingStep s in CookingSteps.Where(x => x.RecipeId == idRecipe && x.Step > UnitOfWork.Repository.GetById<CookingStep>(id).Step))
            {
                s.Step--;
            }
            UnitOfWork.Repository.Delete(UnitOfWork.Repository.GetById<CookingStep>(id));
        }
    }
}
