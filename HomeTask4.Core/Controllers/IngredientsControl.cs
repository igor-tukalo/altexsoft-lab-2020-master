using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace HomeTask4.Core.Controllers
{
    public class IngredientsControl : BaseController, IIngredientsController
    {
        private List<Ingredient> Ingredients => UnitOfWork.Repository.GetList<Ingredient>();
        public IngredientsControl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Add()
        {
            Console.Write("\n    Enter name ingredient: ");
            string name = IsNameMustNotExist(Console.ReadLine());
            string nameIngredient = name;
            UnitOfWork.Repository.Add(new Ingredient() { Name = nameIngredient });
        }

        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        public List<EntityMenu> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            int counterBatch = 1;
            foreach (IEnumerable<Ingredient> ingr in Ingredients.OrderBy(x => x.Name).Batch(int.Parse(ConfigurationManager.AppSettings.Get("Batch"), CultureInfo.CurrentCulture)))
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
            string newName = IsNameMustNotExist(Console.ReadLine());
            Ingredient ingredient = UnitOfWork.Repository.GetById<Ingredient>(id);
            ingredient.Name = newName;
            UnitOfWork.Repository.Update(ingredient);
        }
        public void Delete(int id)
        {
            Console.Write("    Do you really want to remove the ingredient? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            UnitOfWork.Repository.Delete(UnitOfWork.Repository.GetById<Ingredient>(id));
        }

        private string IsNameMustNotExist(string name)
        {
            while (Ingredients.Exists(x => x.Name.ToLower(CultureInfo.CurrentUICulture) == name.ToLower(CultureInfo.CurrentUICulture)))
            {
                Console.Write("    This name is already in use. enter another name: ");
                name = ValidManager.NullOrEmptyText(Console.ReadLine());
            }
            return name;
        }
    }
}
