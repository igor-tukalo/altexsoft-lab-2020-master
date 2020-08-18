using System;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            MainMenuControl mainMenuControl = new MainMenuControl();
            mainMenuControl.GetMenuItems();
        }
    }
}
