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
        private List<EntityMenu> ItemsMenu { get; set; }

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
        private async Task<List<EntityMenu>> GetIngredientsBatch(List<EntityMenu> itemsMenu, int idBatch = 1)
        {
            List<IEnumerable<Ingredient>> ingredientsBatch = await _ingredientsController.GetItemsBatchAsync();
            int countBatch = ingredientsBatch.Count;
            if (idBatch > ingredientsBatch.Count || idBatch < 0)
            {
                idBatch = await ValidationNavigation.BatchExist(idBatch, countBatch);
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

        private async Task AddIngredient()
        {
            Console.Write("\n    Enter name ingredient: ");
            string name = await ValidationNavigation.NullOrEmptyText(Console.ReadLine());
            await _ingredientsController.AddAsync(name);
            await ShowMenu();
        }

        private async Task GoToPage()
        {
            Console.Write("\n    Enter page number: ");
            try
            {
                pageIngredients = int.Parse(Console.ReadLine());
                await ShowMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    {ex.Message} Press any key...");
                Console.ReadKey();
            }
        }

        private async Task ShowContextMenu(int id)
        {
            Task task;
            do
            {
                task = _ingredientsContextMenuNavigation.ShowMenu(ItemsMenu[id].Id);
                await task;
            }
            while (!task.IsCompleted);
            await ShowMenu();
        }
        #endregion

        #region public methods
        public async Task ShowMenu()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add ingredient" },
                    new EntityMenu(){ Name = "    Return to settings"},
                    new EntityMenu(){ Name = "    Go to page", TypeEntity="pages"},
                    new EntityMenu(){ Name = "\n    Ingredients:\n" }
                };
            ItemsMenu = await GetIngredientsBatch(ItemsMenu, pageIngredients);
            await CallNavigation(ItemsMenu, SelectMethodMenu);
        }

        public async Task SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddIngredient();
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {
                        await GoToPage();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
                        {
                            await ShowContextMenu(id);
                        }
                        else
                        {
                            await ShowMenu();
                        }
                    }
                    break;
            }
            #endregion
        }
    }
}
