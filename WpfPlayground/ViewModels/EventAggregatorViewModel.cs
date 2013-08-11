using Caliburn.Micro;
using WpfPlayground.Common;

namespace WpfPlayground.ViewModels
{
    public class EventAggregatorViewModel : PropertyChangedBase
    {        
        public EventAggregatorViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }        

        private readonly IEventAggregator _eventAggregator;
        

        public void CriticalAction()
        {
            var specialEvent = new NotificationEvent("Critical Header", "A critical message.", EventLevel.Critical);
            _eventAggregator.Publish(specialEvent);
        }

        public void ErrorAction()
        {
            var specialEvent = new NotificationEvent("Error Header", "An error message.", EventLevel.Error);
            _eventAggregator.Publish(specialEvent);
        }

        public void WarningAction()
        {
            var specialEvent = new NotificationEvent("Warning Header", "A warning message.", EventLevel.Warning);
            _eventAggregator.Publish(specialEvent);
        }

        public void TrivialAction()
        {
            var specialEvent = new NotificationEvent("Trivial Header", "A trivial message.", EventLevel.Trivial);
            _eventAggregator.Publish(specialEvent);
        }
    }
}
