using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly IEventAggregator _eventAggregator;
        
        public ShellViewModel()
        {
            _eventAggregator = IoC.Get<EventAggregator>();            
            NotificationViewModel = new NotificationViewModel(_eventAggregator);            
        }

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

        public void ShowAnimationButton()
        {
            ActivateItem(new AnimationViewModel());
        }

        public void ShowDataTemplateButton()
        {
            ActivateItem(new DataTemplateViewModel());
        }

        public void ShowEventAggregatorButton()
        {
            // TODO: Use dependency injection instead of resolving
            var eventAggregatorViewModel = new EventAggregatorViewModel(_eventAggregator);            
            ActivateItem(eventAggregatorViewModel);
        }

        public NotificationViewModel NotificationViewModel { get; private set; }
    }
}
