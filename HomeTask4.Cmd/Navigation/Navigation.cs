using HomeTask4.Core.Entities;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation
{
    internal class Navigation
    {
        public delegate void Method(int id);

        private List<EntityMenu> MenuItems;
        private int counter;

        /// <summary>
        /// Navigation menu
        /// </summary>
        /// <param name="menuItems"></param>
        /// <param name="selectedMethod">the method that is executed when the menu is selected</param>
        public void CallNavigation(List<EntityMenu> menuItems, Method selectedMethod)
        {
            MenuItems = menuItems;
            int menuResult;

            List<Method> methodsMenu = new List<Method>();
            for (int i = 0; i < MenuItems.Count; i++)
            {
                methodsMenu.Add(selectedMethod);
            }

            do
            {
                menuResult = PrintMenu();
                methodsMenu[menuResult](menuResult);
                Console.ReadKey();
            } while (menuResult != MenuItems.Count - 1);
        }

        private int PrintMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                ClearArea(0, 0, 6, 0);
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    if (counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(MenuItems[i].Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.WriteLine(MenuItems[i].Name);
                    }
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1)
                    {
                        counter = MenuItems.Count - 1;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == MenuItems.Count)
                    {
                        counter = 0;
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return counter;
        }

        private static void ClearArea(int top, int left, int height, int width)
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
        }
    }
}
