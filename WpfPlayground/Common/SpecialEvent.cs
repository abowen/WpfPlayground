namespace WpfPlayground.Common
{
    public class SpecialEvent
    {
        public SpecialEvent(string heading, string message, EventLevel eventLevel)
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
