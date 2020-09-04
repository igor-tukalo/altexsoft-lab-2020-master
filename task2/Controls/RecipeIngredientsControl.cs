using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class RecipeIngredientsControl : BaseControl, IRecipeIngredientsControl
    {
        public int IdRecipe { get; set; }

        readonly AmountIngredientRepository amountIngredientRepository;
        readonly IngredientRepository ingredientRepository;
        public RecipeIngredientsControl(int idRecipe,IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            IdRecipe = idRecipe;
            amountIngredientRepository = UnitOfWork.AmountIngredients;
            ingredientRepository = UnitOfWork.Ingredients;
        }

        public void Add(int idIngredient)
        {
            int idAmountIngredients = amountIngredientRepository.Items.Count() > 0 ? amountIngredientRepository.Items.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter the amount of ingredient: ");
            double amount = Validation.ValidDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("    Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.AmountIngredients.Create(new AmountIngredient { Id = idAmountIngredients, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = IdRecipe });
            UnitOfWork.SaveAllData();
        }

        public void Delete(int id)
        {
            UnitOfWork.AmountIngredients.Delete(id);
            UnitOfWork.SaveAllData();
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (amountIngredientRepository.Items != null)
                foreach (var a in amountIngredientRepository.Items.Where(x => x.IdRecipe == idRecipe))
                {
                    foreach (var i in ingredientRepository.Items.Where(x => x.Id == a.IdIngredient))
                    {
                        itemsMenu.Add(new EntityMenu() { Id = a.Id, Name = $"    {i.Name} - {a.Amount} {a.Unit}", ParentId = a.IdRecipe, TypeEntity = "ingrRecipe" });
                    }
                }
            return itemsMenu;
        }
    }
}

