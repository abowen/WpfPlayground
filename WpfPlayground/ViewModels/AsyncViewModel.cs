using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class AsyncViewModel : PropertyChangedBase
    {
        private decimal _longProcessProgressBarProcess;

        public decimal LongProcessProgressBarProcess
        {
            get { return _longProcessProgressBarProcess; }
            set
            {
                _longProcessProgressBarProcess = value;
                NotifyOfPropertyChange(() => LongProcessProgressBarProcess);
            }
        }

        public void LongProcessButton()
        {
            StartLongProcess();
            // TODO: Move to Notification Area
            MessageBox.Show("Long Process Started");
        }

        private async void StartLongProcess()
        {
            await Task.Run(() => LongProcess());
            // TODO: Move to Notification Area
            MessageBox.Show("Long Process Finished");
        }

        private void LongProcess()
        {
            LongProcessProgressBarProcess = 0m;
            while (LongProcessProgressBarProcess < 100)
            {
                Thread.Sleep(100);
                LongProcessProgressBarProcess += 0.5m;
            }
        }

    }
}
