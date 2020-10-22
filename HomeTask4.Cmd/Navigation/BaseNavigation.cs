using HomeTask4.Core.Interfaces.Navigation;

namespace HomeTask4.Cmd.Navigation
{
    public abstract class BaseNavigation
    {
        protected IConsoleHelper ValidationNavigation { get; }
        protected BaseNavigation(IConsoleHelper validationNavigation)
        {
            ValidationNavigation = validationNavigation;
        }
    }
}
