using HomeTask4.Core.CRUD;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel;
using System;
using System.Collections.Generic;
using task2.ViewNavigation.ContextMenuNavigation;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class IngredientsNavigation : BaseNavigation, INavigation
    {
        private readonly Validation ValidManager = new Validation();
        private readonly IIngredientsControl Ingredients;
        public IngredientsNavigation(IIngredientsControl ingredients)
        {
            Ingredients = ingredients;
        }
        public int PageIngredients = 1;
        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add ingredient" },
                    new EntityMenu(){ Name = "    Return to settings"},
                    new EntityMenu(){ Name = "    Go to page", TypeEntity="pages"},
                    new EntityMenu(){ Name = "\n    Ingredients:\n" }
                };
            ItemsMenu = Ingredients.GetIngredientsBatch(ItemsMenu, PageIngredients);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Ingredients.Add();
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        SettingsNavigation settNav = new SettingsNavigation(new SettingsControl(UnitOfWork));
                        new ProgramMenu(settNav).CallMenu();
                    }
                    break;
                case 2:
                    {
                        Console.Write("\n    Enter page number: ");
                        PageIngredients = ValidManager.BatchExist(Console.ReadLine(), ItemsMenu[id].ParentId);
                        CallNavigation();
                    }
                    break;
                default:
                    {
                        IngredientsContextMenuNavigation ingContextMenuNNav = new IngredientsContextMenuNavigation(ItemsMenu[id].Id, PageIngredients, Ingredients);
                        new ProgramMenu(ingContextMenuNNav).CallMenu();
                    }
                    break;
            }
        }

    }
}
