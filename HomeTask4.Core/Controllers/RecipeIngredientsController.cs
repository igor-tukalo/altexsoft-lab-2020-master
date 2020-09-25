using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTask4.Core.Controllers
{
    public class RecipeIngredientsController : BaseController, IRecipeIngredientsController
    {
        private List<AmountIngredient> AmountIngredients => UnitOfWork.Repository.GetList<AmountIngredient>();
        private List<Ingredient> Ingredients => UnitOfWork.Repository.GetList<Ingredient>();
        public RecipeIngredientsController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Add(int idIngredient, int idRecipe)
        {
            Console.Write("\n    Enter the amount of ingredient: ");
            double amount = ValidManager.ValidDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("    Enter the unit of ingredient: ");
            string unit = ValidManager.NullOrEmptyText(Console.ReadLine());
            UnitOfWork.Repository.Add(new AmountIngredient { Amount = amount, Unit = unit, IngredientId = idIngredient, RecipeId = idRecipe });
        }

        public void Delete(int id)
        {
            UnitOfWork.Repository.Delete(UnitOfWork.Repository.GetById<AmountIngredient>(id));
        }

        public List<EntityMenu> GetItems(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (AmountIngredients != null)
            {
                foreach (AmountIngredient a in AmountIngredients.Where(x => x.RecipeId == idRecipe))
                {
                    foreach (Ingredient i in Ingredients.Where(x => x.Id == a.IngredientId))
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

