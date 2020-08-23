using System;
using task2.Instruments;
using task2.Models;

namespace task2.Controls
{
    public class SettingsControl : MenuNavigation
    {
        public void GetMenuItems()
        {
            Console.Clear();

            ItemsMenu.Add(new Category(name: "  Customize сategories"));
            ItemsMenu.Add(new Category(name: "  Customize ingredients"));
            ItemsMenu.Add(new Category(name: "  Return to main menu"));

            CallMenuNavigation();
        }

        override protected void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        CategoriesControl categoryControl = new CategoriesControl();
                        categoryControl.GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        IngredientsControl ingredientControl = new IngredientsControl();
                        ingredientControl.GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        MainMenuControl mainMenuControl = new MainMenuControl();
                        mainMenuControl.GetMenuItems();
                    }
                    break;
            }
        }
    }
}
