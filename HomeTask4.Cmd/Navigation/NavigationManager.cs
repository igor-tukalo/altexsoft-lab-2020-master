using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation
{
    public class NavigationManager : BaseNavigation
    {
        private List<EntityMenu> _menuItems;
        private int _counter;
        protected delegate Task MenuMethodsCallback(int id);

        public NavigationManager(IValidationNavigation validationNavigation) : base(validationNavigation)
        {
        }

        private async Task<int> PrintMenuAsync()
        {
            ConsoleKeyInfo key;
            do
            {
                await ClearAreaAsync(0, 0, 6, 0);
                for (int i = 0; i < _menuItems.Count; i++)
                {
                    if (_counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(_menuItems[i].Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.WriteLine(_menuItems[i].Name);
                    }
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    _counter--;
                    if (_counter == -1)
                    {
                        _counter = _menuItems.Count - 1;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    _counter++;
                    if (_counter == _menuItems.Count)
                    {
                        _counter = 0;
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return _counter;
        }

        private Task ClearAreaAsync(int top, int left, int height, int width)
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
        /// Сall navigation menu with method binding for each menu.
        /// </summary>
        /// <param name="menuItems">list of menu items</param>
        /// <param name="selectedMethod">the method that is executed when the menu is selected</param>
        protected async Task CallNavigationAsync(List<EntityMenu> menuItems, MenuMethodsCallback selectedMethod)
        {
            _menuItems = menuItems;
            int menuResult;

            List<MenuMethodsCallback> methodsMenu = new List<MenuMethodsCallback>();
            for (int i = 0; i < _menuItems.Count; i++)
            {
                methodsMenu.Add(selectedMethod);
            }
            do
            {
                menuResult = await PrintMenuAsync();
                await methodsMenu[menuResult](menuResult);
                break;
            } while (menuResult != _menuItems.Count - 1);
        }
    }
}
