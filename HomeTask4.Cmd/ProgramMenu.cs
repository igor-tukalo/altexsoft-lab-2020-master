using HomeTask4.Core.Interfaces;

namespace HomeTask4.Cmd
{
    internal class ProgramMenu
    {
        private INavigation NavigationMenu { get; set; }
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
