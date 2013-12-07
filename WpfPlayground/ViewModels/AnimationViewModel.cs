using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class AnimationViewModel : PropertyChangedBase
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; NotifyOfPropertyChange(() => IsBusy); }
        }

        public void FadeButton()
        {
            IsBusy = !IsBusy;
        }
    }
}
