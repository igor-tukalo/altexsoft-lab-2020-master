using System;
using task2.Instruments;
using task2.Models;

namespace task2.Controls
{
    public class MainMenuControl : MenuNavigation
    {
        override public void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();
            Console.WriteLine(@"
                 _.--._  _.--._
            ,-=.-':;:;:;\':;:;:;'-._
            \\\:;:;:;:;:;\:;:;:;:;:;\
             \\\:;:;:;:;:;\:;:;:;:;:;\
              \\\:;:;:;:;:;\:;:;:;:;:;\
               \\\:;:;:;:;:;\:;::;:;:;:\
                \\\;:;::;:;:;\:;:;:;::;:\
                 \\\;;:;:_:--:\:_:--:_;:;\    Welcome to the Cook Book!
                  \\\_.-'      :      ''-.\
                   \`_..--''--.;.--'''--.._\
                    ");

            ItemsMenu.Add(new Category(name: "  Recipes"));
            ItemsMenu.Add(new Category(name: "  Settings"));

            CallMenuNavigation();

        }
        override protected void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        RecipesCategoryControl recipeControl = new RecipesCategoryControl();
                        recipeControl.GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        SettingsControl settingsControl = new SettingsControl();
                        settingsControl.GetMenuItems();
                    }
                    break;
            }
        }
    }
}
