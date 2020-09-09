using task2.Interfaces;

namespace task2
{
    class ProgramMenu
    {
        INavigation NavigationMenu { get; set; }
        public ProgramMenu(INavigation navigation)
        {
            NavigationMenu = navigation;
        }

        public void CallMenu()
        {
            NavigationMenu.CallNavigation();
        }
    }
}