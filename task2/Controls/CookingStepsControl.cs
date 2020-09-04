using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class CookingStepsControl : BaseControl, ICookingStepsControl
    {
        readonly CookingStepRepository cookingStepRepository;
        public int IdRecipe { get; set; }

        public CookingStepsControl(int idRecipe,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            IdRecipe = idRecipe;
            cookingStepRepository = UnitOfWork.CookingSteps;
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (cookingStepRepository.Items != null)
                foreach (var s in cookingStepRepository.Items.Where(x => x.IdRecipe == idRecipe))
                {
                    itemsMenu.Add(new EntityMenu() { Id = s.Id, Name = $"    {s.Step}. {s.Name}", ParentId = s.IdRecipe });
                }
            return itemsMenu;
        }

        public void Add()
        {
            int idCookingStep = cookingStepRepository.Items.Count() > 0 ? cookingStepRepository.Items.Max(x => x.Id) + 1 : 1;
            int CurrentStep = cookingStepRepository.Items.Where(x => x.IdRecipe == IdRecipe).Count() > 0 ?
                cookingStepRepository.Items.Where(x => x.IdRecipe == IdRecipe).Max(x => x.Step) + 1 : 1;
            Console.Write($"\n    Describe the cooking step {CurrentStep}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            cookingStepRepository.Create(new CookingStep() { Id = idCookingStep, Step = CurrentStep, Name = stepName, IdRecipe = IdRecipe });
            UnitOfWork.SaveAllData();
            Console.Write("\n    Add another cooking step? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            Add();
        }

        public void Edit(int id)
        {
            var cookingStep = cookingStepRepository.Get(id);
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            cookingStepRepository.Update(cookingStep);
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            foreach (var s in cookingStepRepository.Items.Where(x => x.IdRecipe == IdRecipe && x.Step > cookingStepRepository.Get(id).Step))
            {
                s.Step--;
            }
            cookingStepRepository.Delete(id);
            UnitOfWork.SaveAllData();
        }
    }
}
