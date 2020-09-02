﻿using System;
using System.Collections.Generic;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class RecipeIngredientsControl : BaseControl, IRecipeIngredientsControl
    {
        public List<AmountIngredient> AmountIngredients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int IdRecipe { get; set; }
        public RecipeIngredientsControl(int idRecipe)
        {
            IdRecipe = idRecipe;
            Ingredients = DBControl.Ingredients.Items;
            AmountIngredients = DBControl.AmountIngredients.Items;
        }

        public void Add(int idIngredient)
        {
            int idAmountIngredients = AmountIngredients.Count() > 0 ? AmountIngredients.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter the amount of ingredient: ");
            double amount = Validation.ValidDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("    Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());
            DBControl.AmountIngredients.Create(new AmountIngredient { Id = idAmountIngredients, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = IdRecipe });
            base.Add();
        }

        public override void Delete(int id)
        {
            DBControl.AmountIngredients.Delete(id);
            base.Delete(id);
        }

        public List<EntityMenu> Get(List<EntityMenu> itemsMenu, int idRecipe)
        {
            if (AmountIngredients != null)
                foreach (var a in AmountIngredients.Where(x => x.IdRecipe == idRecipe))
                {
                    foreach (var i in Ingredients.Where(x => x.Id == a.IdIngredient))
                    {
                        itemsMenu.Add(new EntityMenu() { Id = a.Id, Name = $"    {i.Name} - {a.Amount} {a.Unit}", ParentId = a.IdRecipe, TypeEntity = "ingrRecipe" });
                    }
                }
            return itemsMenu;
        }
    }
}
