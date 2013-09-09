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

        public void ShowBindingButton()
        {
            ActivateItem(new BindingViewModel());
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

        public void ShowUniformGridButton()
        {
            ActivateItem(new UniformGridViewModel());
        }

        public void ShowNotifyButton()
        {
            ActivateItem(new NotifyViewModel());
        }

        public void ShowProcessButton()
        {
            ActivateItem(new ProcessViewModel());
        }

        public NotificationViewModel NotificationViewModel { get; private set; }
    }
}
