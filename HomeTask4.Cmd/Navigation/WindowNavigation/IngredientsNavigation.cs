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
        private readonly IIngredientsController _ingredientsController;
        private readonly IIngredientsContextMenuNavigation _ingredientsContextMenuNavigation;
        protected int PageIngredients { get; set; } = 1;
        protected List<EntityMenu> ItemsMenu { get; set; }


        public IngredientsNavigation(IConsoleHelper validationNavigation,
            IIngredientsController ingredientsController,
            IIngredientsContextMenuNavigation ingredientsContextMenuNavigation) : base(validationNavigation)
        {
            _ingredientsController = ingredientsController;
            _ingredientsContextMenuNavigation = ingredientsContextMenuNavigation;
        }

        #region protected methods
        /// <summary>
        /// Get the ingredients of the specified batch
        /// </summary>
        /// <param name="itemsMenu"></param>
        /// <param name="idBatch"></param>
        protected async Task<List<EntityMenu>> GetIngredientsBatchAsync(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            try
            {
                List<IEnumerable<Ingredient>> ingredientsBatch = await _ingredientsController.GetIngredientsBatchAsync();
                int countBatch = ingredientsBatch.Count;
                if (idBatch > ingredientsBatch.Count || idBatch < 0)
                {
                    PageIngredients = await ValidationNavigation.BatchExistAsync(idBatch, countBatch);
                    idBatch = PageIngredients;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("    Press any key...");
                Console.ReadKey();
            }
            return itemsMenu;
        }

        protected async Task AddIngredientAsync()
        {
            Console.Write("\n    Enter name ingredient: ");
            string name = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _ingredientsController.AddAsync(name);
        }

        protected async Task GoToPageAsync()
        {
            Console.Write("\n    Enter page number: ");
            try
            {
                PageIngredients = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    {ex.Message} Press any key...");
                Console.ReadKey();
            }
        }

        protected virtual async Task ShowContextMenuAsync(int id)
        {
            await _ingredientsContextMenuNavigation.ShowMenuAsync(ItemsMenu[id].Id);
            await ShowMenuAsync();
        }
        #endregion

        #region public methods
        public virtual async Task ShowMenuAsync()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add ingredient" },
                    new EntityMenu(){ Name = "    Return to the previous menu"},
                    new EntityMenu(){ Name = "    Go to page", TypeEntity="pages"},
                    new EntityMenu(){ Name = "\n    Ingredients:\n" }
                };
            ItemsMenu = await GetIngredientsBatchAsync(ItemsMenu, PageIngredients);
            await CallNavigationAsync(ItemsMenu, SelectMethodMenuAsync);
        }

        public virtual async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddIngredientAsync();
                        await ShowMenuAsync();
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {
                        await GoToPageAsync();
                        await ShowMenuAsync();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
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
