using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.CRUD
{
    public class CookingStepsControl : BaseControl, ICookingStepsControl
    {
        private List<CookingStep> CookingSteps => UnitOfWork.Repository.GetListAsync<CookingStep>().Result;

        public CookingStepsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
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
            UnitOfWork.Repository.AddAsync(new CookingStep() { Step = CurrentStep, Name = stepName, RecipeId = idRecipe });
            Console.Write("\n    Add another cooking step? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            Add(idRecipe);
        }

        public void Edit(int id)
        {
            CookingStep cookingStep = UnitOfWork.Repository.GetByIdAsync<CookingStep>(id).Result;
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = ValidManager.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            UnitOfWork.Repository.UpdateAsync(cookingStep);
        }

        public void Delete(int id, int idRecipe)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            foreach (CookingStep s in CookingSteps.Where(x => x.RecipeId == idRecipe && x.Step > UnitOfWork.Repository.GetByIdAsync<CookingStep>(id).Result.Step))
            {
                s.Step--;
            }
            UnitOfWork.Repository.DeleteAsync(UnitOfWork.Repository.GetByIdAsync<CookingStep>(id).Result);
        }
    }
}
