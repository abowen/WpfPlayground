using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Caliburn.Micro;
using System.Threading;

namespace WpfPlayground.ViewModels
{
    public class ProcessViewModel : PropertyChangedBase
    {
        public ProcessViewModel()
        {            
            var processes = Process.GetProcesses().ToList();
            var processEntities = processes
                .Select(p => new ProcessEntity()
                {
                    ProcessName = p.ProcessName,
                    MemoryUsage = p.PagedMemorySize64
                })
                .OrderByDescending(p => p.MemoryUsageMegabytes);
            Processes = new ObservableCollection<ProcessEntity>(processEntities);

            AsyncRefreshProcesses();

            // Code snippet from WPF Cookbook 4.5
            //Processes = new CollectionViewSource {Source = processes};
            //var view = CollectionViewSource.GetDefaultView(Processes);
            //view.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            //var liveView = (ICollectionViewLiveShaping) CollectionViewSource.GetDefaultView(Processes);
            //liveView.IsLiveSorting = true;
        }

        public ObservableCollection<ProcessEntity> Processes { get; private set; }

        public async void AsyncRefreshProcesses()
        {
            await Task.Run(() => RefreshProcesses());
        }

        private void RefreshProcesses()
        {            
            while (true)
            {
                Thread.Sleep(5000);
                var processes = Process.GetProcesses().ToList();
                foreach (var processEntity in Processes)
                {
                    var process = processes.FirstOrDefault(p => p.ProcessName == processEntity.ProcessName);
                    if (process != null)
                    {
                        processEntity.MemoryUsage = process.PagedMemorySize64;
                    }
                }
            }
        }
    }

    public class ProcessEntity : PropertyChangedBase
    {
        private string _processName;
        public string ProcessName
        {
            get { return _processName; }
            set
            {
                _processName = value;
                NotifyOfPropertyChange(() => ProcessName);
            }
        }

        private decimal _memoryUsage;
        public decimal MemoryUsage
        {
            get { return _memoryUsage; }
            set
            {
                _memoryUsage = value;
                NotifyOfPropertyChange(() => MemoryUsageMegabytes);
                NotifyOfPropertyChange(() => MemoryUsageGigabytes);
            }
        }
         

           public decimal MemoryUsageMegabytes
        {
            get
            {
                return MemoryUsage / 1048576;
            }
        }

        public decimal MemoryUsageGigabytes
        {
            get
            {
                return MemoryUsageMegabytes / 1024;
            }
        }

    }
}
