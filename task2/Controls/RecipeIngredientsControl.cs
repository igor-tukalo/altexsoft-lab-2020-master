using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    class RecipeIngredientsControl : IRecipeIngredientsControl
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public int IdRecipe { get; set; }
        public RecipeIngredientsControl(int idRecipe)
        {
            IdRecipe = idRecipe;
        }

        public void Add(int idRecipe, int idIngredient)
        {
            int idAmountIngredients = unitOfWork.AmountIngredients.GetAll().Count() > 0 ? unitOfWork.AmountIngredients.GetAll().Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter the amount of ingredient: ");
            double amount = Validation.ValidDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("    Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());
            unitOfWork.AmountIngredients.Create(new AmountIngredient { Id = idAmountIngredients, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = idRecipe });
            unitOfWork.SaveDataTable("AmountIngredients.json", JsonConvert.SerializeObject(unitOfWork.AmountIngredients.GetAll()));
        }

        public void Delete(int idAmountIngredient)
        {
            unitOfWork.AmountIngredients.Delete(idAmountIngredient);
            unitOfWork.SaveDataTable("AmountIngredients.json", JsonConvert.SerializeObject(unitOfWork.AmountIngredients.GetAll()));
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (unitOfWork.AmountIngredients.GetAll() != null)
                foreach (var a in unitOfWork.AmountIngredients.GetAll().Where(x => x.IdRecipe == idRecipe))
                {
                    foreach (var i in unitOfWork.Ingredients.GetAll().Where(x => x.Id == a.IdIngredient))
                    {
                        itemsMenu.Add(new EntityMenu() { Id = a.Id, Name = $"    {i.Name} - {a.Amount} {a.Unit}", ParentId=a.IdRecipe, TypeEntity = "ingrRecipe" });
                    }
                }
            return itemsMenu;
        }
    }
}

