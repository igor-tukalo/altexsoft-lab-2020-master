using Newtonsoft.Json;
using System;
using task2.Controls;
using task2.Models;
using task2.Repositories;

namespace task2.Instruments
{
    public class ContextMenuIngredients : ContextMenuNavigation
    {
        public ContextMenuIngredients(UnitOfWork _unitOfWork, int idMenuNavigation, int recipeId = 0) : base(_unitOfWork, idMenuNavigation, recipeId)
        {
        }

        protected override void Rename()
        {
            Console.Write("  Enter new name: ");
            string newName = Console.ReadLine();


            Ingredient ingredient = unitOfWork.Ingredients.Get(IdMenuNavigation);
            unitOfWork.Ingredients.Update(new Ingredient { Id = ingredient.Id, Name = newName});

            unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
            Cancel();
        }

        protected override void Delete()
        {
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                unitOfWork.Ingredients.Delete(IdMenuNavigation);
                unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
                Cancel();
            }
            else Cancel();
        }

        protected override void Cancel()
        {
            new IngredientsControl().GetMenuItems();
        }
    }
}
