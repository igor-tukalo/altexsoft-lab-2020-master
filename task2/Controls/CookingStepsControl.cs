using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class CookingStepsControl : BaseControl, ICookingStepsControl
    {
        public List<CookingStep> CookingSteps { get; set; }
        public int IdRecipe { get; set; }

        public CookingStepsControl(int idRecipe,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            IdRecipe = idRecipe;
            CookingSteps = UnitOfWork.CookingSteps.Items;
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (CookingSteps != null)
                foreach (var s in CookingSteps.Where(x => x.IdRecipe == idRecipe))
                {
                    itemsMenu.Add(new EntityMenu() { Id = s.Id, Name = $"    {s.Step}. {s.Name}", ParentId = s.IdRecipe });
                }
            return itemsMenu;
        }

        public void Add()
        {
            int idCookingStep = CookingSteps.Count() > 0 ? CookingSteps.Max(x => x.Id) + 1 : 1;
            int CurrentStep = CookingSteps.Where(x => x.IdRecipe == IdRecipe).Count() > 0 ?
                CookingSteps.Where(x => x.IdRecipe == IdRecipe).Max(x => x.Step) + 1 : 1;
            Console.Write($"\n    Describe the cooking step {CurrentStep}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.CookingSteps.Create(new CookingStep() { Id = idCookingStep, Step = CurrentStep, Name = stepName, IdRecipe = IdRecipe });
            UnitOfWork.SaveAllData();
            Console.Write("\n    Add another cooking step? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            Add();
        }

        public void Edit(int id)
        {
            var cookingStep = UnitOfWork.CookingSteps.Get(id);
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            UnitOfWork.CookingSteps.Update(cookingStep);
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            foreach (var s in CookingSteps.Where(x => x.IdRecipe == IdRecipe && x.Step > UnitOfWork.CookingSteps.Get(id).Step))
            {
                s.Step--;
            }
            UnitOfWork.CookingSteps.Delete(id);
            UnitOfWork.SaveAllData();
        }
    }
}
