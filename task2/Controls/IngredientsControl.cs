using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using task2.Interfaces;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    class IngredientsControl : IIngredientsControl
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();

        public void Add()
        {
            try
            {
                int id = unitOfWork.Ingredients.GetAll().Max(x => x.Id) + 1;

                Console.Write("\n    Enter name ingredient: ");
                string name = unitOfWork.Ingredients.IsNameMustNotExist(Console.ReadLine());
                string nameIngredient = name;
                unitOfWork.Ingredients.Create(new Ingredient() { Id = id, Name = nameIngredient });
                unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }

        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        public List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            int counterBatch = 1;
            foreach (var ingr in unitOfWork.Ingredients.GetAll().OrderBy(x => x.Name).Batch(int.Parse(ConfigurationManager.AppSettings.Get("Batch"))))
            {
                if (counterBatch == idBatch)
                    foreach (var batch in ingr)
                    {
                        itemsMenu.Add(new EntityMenu() { Id = batch.Id, Name = $"    {batch.Name}", TypeEntity="ingr" });
                    }
                counterBatch++;
            }
            return itemsMenu = itemsMenu
            .Select(i => i.TypeEntity == "pages"
            ? new EntityMenu { Name = $"    Go to page. Pages: {idBatch}/{counterBatch}", ParentId = counterBatch, TypeEntity = "pages" }
            : i).ToList();
        }
        public void Rename(int idIngredient)
        {
            Console.Write("    Enter new name: ");
            string newName = unitOfWork.Ingredients.IsNameMustNotExist(Console.ReadLine());
            var ingredient = unitOfWork.Ingredients.Get(idIngredient);
            ingredient.Name = newName;
            unitOfWork.Ingredients.Update(ingredient);
            unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
        }
        public void Delete(int idIngredient)
        {
            Console.Write("    Do you really want to remove the ingredient? ");
            if (Validation.YesNo() == ConsoleKey.Y)
            {
                unitOfWork.Ingredients.Delete(idIngredient);
                unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
            }
        }
    }
}
