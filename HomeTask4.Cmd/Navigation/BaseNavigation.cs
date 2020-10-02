using HomeTask4.Core.Interfaces.Navigation;

namespace HomeTask4.Cmd.Navigation
{
    public abstract class BaseNavigation
    {
        protected IValidationNavigation ValidationNavigation { get; }
        protected BaseNavigation(IValidationNavigation validationNavigation)
        {
            ValidationNavigation = validationNavigation;
        }
    }
}
