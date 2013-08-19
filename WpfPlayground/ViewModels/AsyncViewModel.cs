using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class AsyncViewModel : PropertyChangedBase
    {
        private decimal _asyncProgressBarProcess;

        public decimal AsyncProgressBarProcess
        {
            get { return _asyncProgressBarProcess; }
            set
            {
                _asyncProgressBarProcess = value;
                NotifyOfPropertyChange(() => AsyncProgressBarProcess);
            }
        }

        public void AsyncButton()
        {
            StartAsync();            
        }

        private async void StartAsync()
        {
            await Task.Run(() => AsyncProcess());            
        }

        private void AsyncProcess()
        {
            AsyncProgressBarProcess = 0m;
            while (AsyncProgressBarProcess < 100)
            {
                Thread.Sleep(100);
                AsyncProgressBarProcess += 0.5m;
            }
        }

        public void TaskButton()
        {
            TaskProcess();
        }

        private void TaskProcess()
        {
            var taskSquareRoot = Task<List<double>>.Factory.StartNew(() =>
            {
                SquareRootCount = 0;
                var results = new List<double>();
                for (int i = 1; i <= 10000; i++)
                {
                    var root = Math.Sqrt(i);
                    var equalizer = (root%1);
                    if (equalizer == 0)
                    {
                        results.Add(root);
                        SquareRootNumber = i;
                        Thread.Sleep(100);
                    }
                }
                return results;
            });

            taskSquareRoot.ContinueWith(task =>                     
            {
                SquareRootCount = task.Result.Count;
            });   
         
                        
        }

        private double _squareRootNumber;
        public double SquareRootNumber
        {
            get { return _squareRootNumber; }
            set
            {
                _squareRootNumber = value;
                NotifyOfPropertyChange(() => SquareRootNumber);
            }
        }

        private int _squareRootCount;
        public int SquareRootCount
        {
            get { return _squareRootCount; }
            set
            {
                _squareRootCount = value;
                NotifyOfPropertyChange(() => SquareRootCount);
            }
        }

    }
}
