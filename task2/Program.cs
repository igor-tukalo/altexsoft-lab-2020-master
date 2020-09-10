using System;
using task2.ViewNavigation.WindowNavigation;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = Console.LargestWindowHeight;
            ProgramMenu programMenu = new ProgramMenu(new MainWindowNavigation());
            programMenu.CallMenu();
        }
    }
}