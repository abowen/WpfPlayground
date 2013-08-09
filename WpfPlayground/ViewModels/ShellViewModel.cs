using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public void ShowHomeButton()
        {
            ActivateItem(new HomeViewModel());
        }

        public void ShowThemeButton()
        {
            ActivateItem(new ThemeViewModel());
        }

        public void ShowAsyncButton()
        {
            ActivateItem(new AsyncViewModel());
        }
    }
}
