using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    class CookingStepsControl : ICookingStepsControl
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public int IdRecipe { get; set; }
        public CookingStepsControl(int idRecipe)
        {
            IdRecipe = idRecipe;
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (unitOfWork.CookingSteps.GetAll() != null)
                foreach (var s in unitOfWork.CookingSteps.GetAll().Where(x => x.IdRecipe == idRecipe))
                {
                    itemsMenu.Add(new EntityMenu() { Id = s.Id, Name = $"    {s.Step}. {s.Name}", ParentId = s.IdRecipe });
                }
            return itemsMenu;
        }

        public void Add(int recipeId)
        {
            int idCookingStep = unitOfWork.CookingSteps.GetAll().Count() > 0 ? unitOfWork.CookingSteps.GetAll().Max(x => x.Id) + 1 : 1;
            int CurrentStep = unitOfWork.CookingSteps.GetAll().Where(x => x.IdRecipe == recipeId).Count() > 0 ?
                unitOfWork.CookingSteps.GetAll().Where(x => x.IdRecipe == recipeId).Max(x => x.Step) + 1 : 1;
            Console.Write($"\n    Describe the cooking step {CurrentStep}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            unitOfWork.CookingSteps.Create(new CookingStep() { Id = idCookingStep, Step = CurrentStep, Name = stepName, IdRecipe = recipeId });
            unitOfWork.SaveDataTable("CookingSteps.json", JsonConvert.SerializeObject(unitOfWork.CookingSteps.GetAll()));
            Console.Write("\n    Add another cooking step? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                Add(recipeId);
            }
        }

        public void Edit(int idCookingStep)
        {
            var cookingStep = unitOfWork.CookingSteps.Get(idCookingStep);
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = Validation.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            unitOfWork.CookingSteps.Update(cookingStep);
            unitOfWork.SaveDataTable("CookingSteps.json", JsonConvert.SerializeObject(unitOfWork.CookingSteps.GetAll()));
        }

        public void Delete(int idCookingStep, int idRecipe)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                foreach (var s in unitOfWork.CookingSteps.GetAll().Where(x => x.IdRecipe == idRecipe && x.Step > unitOfWork.CookingSteps.Get(idCookingStep).Step))
                {
                    s.Step--;
                }
                unitOfWork.CookingSteps.Delete(idCookingStep);
                unitOfWork.SaveDataTable("CookingSteps.json", JsonConvert.SerializeObject(unitOfWork.CookingSteps.GetAll()));
            }
        }
    }
}
