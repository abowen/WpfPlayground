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

        public void EventAggregatorButton()
        {
            var specialEvent = new SpecialEvent("EVENT AGGREGATOR", "Some Message", EventLevel.Critical);
            _eventAggregator.Publish(specialEvent);
        }
    }
}
