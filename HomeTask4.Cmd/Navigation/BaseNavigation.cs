using HomeTask4.Cmd.Interfaces;

namespace HomeTask4.Cmd.Navigation
{
    public abstract class BaseNavigation
    {
        protected IConsoleHelper ConsoleHelper { get; }
        protected BaseNavigation(IConsoleHelper consoleHelper)
        {
            ConsoleHelper = consoleHelper;
        }
    }
}
