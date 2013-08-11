using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using WpfPlayground.Common;

namespace WpfPlayground.ViewModels
{
    public class NotificationViewModel : PropertyChangedBase, IHandle<NotificationEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public NotificationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        // Design purposes only
        public NotificationViewModel()
        {
            Header = "Design Header Only";
            Message = "Design Message Only";
            HeaderBrush = new SolidColorBrush(Colors.Pink);
        }

        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        private Brush _headerBrush;

        public Brush HeaderBrush
        {
            get { return _headerBrush; }
            set
            {
                _headerBrush = value;
                NotifyOfPropertyChange(() => HeaderBrush);
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => IsVisible);
            }
        }



        public void Handle(NotificationEvent message)
        {
            Header = message.Heading;
            Message = message.Message;

            switch (message.EventLevel)
            {
                case EventLevel.Critical:
                    HeaderBrush = new SolidColorBrush(Colors.DarkRed);
                    break;
                case EventLevel.Error:
                    HeaderBrush = new SolidColorBrush(Colors.IndianRed);
                    break;
                case EventLevel.Warning:
                    HeaderBrush = new SolidColorBrush(Colors.Gold);
                    break;
                case EventLevel.Trivial:
                    HeaderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                    break;
                default:
                    throw new Exception("Unknown Event Level Raised");
            }

            ShowNotification();
        }

        async private void  ShowNotification()
        {
            IsVisible = true;
            await Task.Run(() => Thread.Sleep(3000));
            IsVisible = false;
        }
    }
}
