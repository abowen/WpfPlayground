using Caliburn.Micro;
using WpfPlayground.ViewModels;

namespace WpfPlayground
{
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
        }     
    }
}
