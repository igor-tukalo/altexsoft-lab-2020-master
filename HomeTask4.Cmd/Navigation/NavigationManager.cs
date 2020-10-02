using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation
{
    public class NavigationManager : BaseNavigation
    {
        protected delegate Task MenuMethodsCallback(int id);
        private List<EntityMenu> menuItems;
        private int counter;

        public NavigationManager(IValidationNavigation validationNavigation) : base(validationNavigation)
        {
        }

        private Task<int> PrintMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                ClearArea(0, 0, 6, 0);
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i].Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.WriteLine(menuItems[i].Name);
                    }
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1)
                    {
                        counter = menuItems.Count - 1;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Count)
                    {
                        counter = 0;
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return Task.FromResult(counter);
        }

        private static Task ClearArea(int top, int left, int height, int width)
        {
            ConsoleColor colorBefore = Console.BackgroundColor;
            try
            {
                Console.BackgroundColor = ConsoleColor.Black;
                string spaces = new string(' ', width);
                for (int i = 0; i < height; i++)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(spaces);
                }
            }
            finally
            {
                Console.BackgroundColor = colorBefore;
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Navigation menu
        /// </summary>
        /// <param name="menuItems"></param>
        /// <param name="selectedMethod">the method that is executed when the menu is selected</param>
        protected async Task CallNavigation(List<EntityMenu> menuItems, MenuMethodsCallback selectedMethod)
        {
            this.menuItems = menuItems;
            int menuResult;

            List<MenuMethodsCallback> methodsMenu = new List<MenuMethodsCallback>();
            for (int i = 0; i < this.menuItems.Count; i++)
            {
                methodsMenu.Add(selectedMethod);
            }
            do
            {
                menuResult = await PrintMenu();
                await methodsMenu[menuResult](menuResult);
                break;
            } while (menuResult != this.menuItems.Count - 1);
        }
    }
}
