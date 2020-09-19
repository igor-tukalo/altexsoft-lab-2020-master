using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.CRUD
{
    public class RecipeIngredientsControl : BaseControl, IRecipeIngredientsControl
    {
        private readonly AmountIngredientRepository amountIngredientRepository;
        private readonly IngredientRepository ingredientRepository;
        public RecipeIngredientsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            amountIngredientRepository = UnitOfWork.AmountIngredients;
            ingredientRepository = UnitOfWork.Ingredients;
        }

        public void Add(int idIngredient, int idRecipe)
        {
            Console.Write("\n    Enter the amount of ingredient: ");
            double amount = ValidManager.ValidDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("    Enter the unit of ingredient: ");
            string unit = ValidManager.NullOrEmptyText(Console.ReadLine());
            amountIngredientRepository.Create(new AmountIngredient { Amount = amount, Unit = unit, IngredientId = idIngredient, RecipeId = idRecipe });
        }

        public void Delete(int id)
        {
            amountIngredientRepository.Delete(amountIngredientRepository.GetItem(id));
        }

        public List<EntityMenu> GetItems(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (amountIngredientRepository.Items != null)
            {
                foreach (AmountIngredient a in amountIngredientRepository.Items.Where(x => x.RecipeId == idRecipe))
                {
                    foreach (Ingredient i in ingredientRepository.Items.Where(x => x.Id == a.IngredientId))
                    {
                        if (itemsMenu != null)
                        {
                            itemsMenu.Add(new EntityMenu() { Id = a.Id, Name = $"    {i.Name} - {a.Amount} {a.Unit}", ParentId = a.RecipeId, TypeEntity = "ingrRecipe" });
                        }
                    }
                }
            }

            return itemsMenu;
        }
    }
}

