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
        readonly IngredientRepository ingredientRepository;
        public IngredientsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ingredientRepository = UnitOfWork.Ingredients;
        }

        public void Add()
        {
            int id = ingredientRepository.Items.Count() > 0 ? ingredientRepository.Items.Max(x => x.Id) + 1 : 1;
            Console.Write("\n    Enter name ingredient: ");
            string name = ingredientRepository.IsNameMustNotExist(Console.ReadLine());
            string nameIngredient = name;
            ingredientRepository.Create(new Ingredient() { Id = id, Name = nameIngredient });
            UnitOfWork.SaveAllData();
        }

        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        public List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            int counterBatch = 1;
            foreach (var ingr in ingredientRepository.Items.OrderBy(x => x.Name).Batch(int.Parse(ConfigurationManager.AppSettings.Get("Batch"))))
            {
                if (counterBatch == idBatch)
                    foreach (var batch in ingr)
                        itemsMenu.Add(new EntityMenu() { Id = batch.Id, Name = $"    {batch.Name}", TypeEntity = "ingr" });
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
            var ingredient = ingredientRepository.Get(id);
            ingredient.Name = newName;
            ingredientRepository.Update(ingredient);
            UnitOfWork.SaveAllData();
        }
        public void Delete(int id)
        {
            Console.Write("    Do you really want to remove the ingredient? ");
            if (Validation.YesNo() == ConsoleKey.N) return;
            ingredientRepository.Delete(id);
            UnitOfWork.SaveAllData();
        }
    }
}
