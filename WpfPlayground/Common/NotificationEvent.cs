namespace WpfPlayground.Common
{
    public class NotificationEvent
    {
        public NotificationEvent(string heading, string message, EventLevel eventLevel)
        {
            Heading = heading;
            Message = message;
            EventLevel = eventLevel;
        }

        public string Heading { get; private set; }
        public string Message { get; private set; }
        public EventLevel EventLevel { get; private set; }
    }
}
