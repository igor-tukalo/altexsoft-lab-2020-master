using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class IngredientsNavigation : NavigationManager, IIngredientsNavigation
    {
        private int pageIngredients = 1;
        private readonly IIngredientsController _ingredientsController;
        private readonly IIngredientsContextMenuNavigation _ingredientsContextMenuNavigation;
        private List<EntityMenu> itemsMenu;

        public IngredientsNavigation(IValidationNavigation validationNavigation,
            IIngredientsController ingredientsController,
            IIngredientsContextMenuNavigation ingredientsContextMenuNavigation) : base(validationNavigation)
        {
            _ingredientsController = ingredientsController;
            _ingredientsContextMenuNavigation = ingredientsContextMenuNavigation;
        }

        #region private methods
        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        private async Task<List<EntityMenu>> GetIngredientsBatchAsync(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            List<IEnumerable<Ingredient>> ingredientsBatch = await _ingredientsController.GetItemsBatchAsync();
            int countBatch = ingredientsBatch.Count;
            if (idBatch > ingredientsBatch.Count || idBatch < 0)
            {
                idBatch = await ValidationNavigation.BatchExistAsync(idBatch, countBatch);
            }
            idBatch--;
            IEnumerable<Ingredient> ingredients = ingredientsBatch.ElementAt(idBatch);
            foreach (Ingredient batch in ingredients)
            {
                if (itemsMenu != null)
                {
                    itemsMenu.Add(new EntityMenu() { Id = batch.Id, Name = $"    {batch.Name}", TypeEntity = "ingr" });
                }
            }
            idBatch++;
            itemsMenu = itemsMenu
            .Select(i => i.TypeEntity == "pages"
            ? new EntityMenu { Name = $"    Go to page. Pages: {idBatch}/{countBatch}", TypeEntity = "pages" }
            : i).ToList();
            return itemsMenu;
        }

        private async Task AddIngredientAsync()
        {
            Console.Write("\n    Enter name ingredient: ");
            string name = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _ingredientsController.AddAsync(name);
            await ShowMenuAsync();
        }

        private async Task GoToPageAsync()
        {
            Console.Write("\n    Enter page number: ");
            try
            {
                pageIngredients = int.Parse(Console.ReadLine());
                await ShowMenuAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    {ex.Message} Press any key...");
                Console.ReadKey();
            }
        }

        private async Task ShowContextMenuAsync(int id)
        {
            await _ingredientsContextMenuNavigation.ShowMenuAsync(itemsMenu[id].Id);
            await ShowMenuAsync();
        }
        #endregion

        #region public methods
        public async Task ShowMenuAsync()
        {
            Console.Clear();
            itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add ingredient" },
                    new EntityMenu(){ Name = "    Return to settings"},
                    new EntityMenu(){ Name = "    Go to page", TypeEntity="pages"},
                    new EntityMenu(){ Name = "\n    Ingredients:\n" }
                };
            itemsMenu = await GetIngredientsBatchAsync(itemsMenu, pageIngredients);
            await CallNavigationAsync(itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddIngredientAsync();
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {
                        await GoToPageAsync();
                    }
                    break;
                default:
                    {
                        if (itemsMenu[id].Id != 0)
                        {
                            await ShowContextMenuAsync(id);
                        }
                        else
                        {
                            await ShowMenuAsync();
                        }
                    }
                    break;
            }
            #endregion
        }
    }
}
