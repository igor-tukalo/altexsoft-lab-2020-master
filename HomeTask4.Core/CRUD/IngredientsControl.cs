using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace HomeTask4.Core.CRUD
{
    public class IngredientsControl : BaseControl, IIngredientsControl
    {
        private readonly IngredientRepository ingredientRepository;
        public IngredientsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ingredientRepository = UnitOfWork.Ingredients;
        }

        public void Add()
        {
            Console.Write("\n    Enter name ingredient: ");
            string name = ingredientRepository.IsNameMustNotExist(Console.ReadLine());
            string nameIngredient = name;
            ingredientRepository.Create(new Ingredient() { Name = nameIngredient });
        }

        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        public List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            int counterBatch = 1;
            foreach (IEnumerable<Ingredient> ingr in ingredientRepository.Items.OrderBy(x => x.Name).Batch(int.Parse(ConfigurationManager.AppSettings.Get("Batch"), CultureInfo.CurrentCulture)))
            {
                if (counterBatch == idBatch)
                {
                    foreach (Ingredient batch in ingr)
                    {
                        if (itemsMenu != null)
                        {
                            itemsMenu.Add(new EntityMenu() { Id = batch.Id, Name = $"    {batch.Name}", TypeEntity = "ingr" });
                        }
                    }
                }

                counterBatch++;
            }
            return itemsMenu = itemsMenu
            .Select(i => i.TypeEntity == "pages"
            ? new EntityMenu { Name = $"    Go to page. Pages: {idBatch}/{counterBatch}", ParentId = counterBatch, TypeEntity = "pages" }
            : i).ToList();
        }
        public void Edit(int id)
        {
            Console.Write("    Enter new name: ");
            string newName = ingredientRepository.IsNameMustNotExist(Console.ReadLine());
            Ingredient ingredient = ingredientRepository.GetItem(id);
            ingredient.Name = newName;
            ingredientRepository.Update(ingredient);
        }
        public void Delete(int id)
        {
            Console.Write("    Do you really want to remove the ingredient? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }

            ingredientRepository.Delete(ingredientRepository.GetItem(id));
        }
    }
}
