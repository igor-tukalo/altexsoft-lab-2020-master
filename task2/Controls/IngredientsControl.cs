using MoreLinq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using task2.Interfaces;
using task2.Models;

namespace task2.Controls
{
    class IngredientsControl : BaseControl, IIngredientsControl
    {
        public List<Ingredient> Ingredients { get; set; }

        public IngredientsControl()
        { 
            Ingredients = UnitOfWork.Ingredients.Items;
        }

        public override void Add()
        {
            int id = Ingredients.Count() > 0 ? Ingredients.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter name ingredient: ");
            string name = UnitOfWork.Ingredients.IsNameMustNotExist(Console.ReadLine());
            string nameIngredient = name;
            UnitOfWork.Ingredients.Create(new Ingredient() { Id = id, Name = nameIngredient });
            base.Add();
        }

        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        public List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            int counterBatch = 1;
            foreach (var ingr in Ingredients.OrderBy(x => x.Name).Batch(int.Parse(ConfigurationManager.AppSettings.Get("Batch"))))
            {
                if (counterBatch == idBatch)
                    foreach (var batch in ingr)
                    {
                        itemsMenu.Add(new EntityMenu() { Id = batch.Id, Name = $"    {batch.Name}", TypeEntity = "ingr" });
                    }
                counterBatch++;
            }
            return itemsMenu = itemsMenu
            .Select(i => i.TypeEntity == "pages"
            ? new EntityMenu { Name = $"    Go to page. Pages: {idBatch}/{counterBatch}", ParentId = counterBatch, TypeEntity = "pages" }
            : i).ToList();
        }
        public override void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = UnitOfWork.Ingredients.IsNameMustNotExist(Console.ReadLine());
            var ingredient = UnitOfWork.Ingredients.Get(id);
            ingredient.Name = newName;
            UnitOfWork.Ingredients.Update(ingredient);
            base.Edit(id);
        }
        public override void Delete(int id)
        {
            Console.Write("    Do you really want to remove the ingredient? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            UnitOfWork.Ingredients.Delete(id);
            base.Delete(id);
        }
    }
}
