using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class HomeViewModel : PropertyChangedBase
    {
        public HomeViewModel()
        {
            UserName = "Andrew Bowen";
        }

        string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
    }
}
