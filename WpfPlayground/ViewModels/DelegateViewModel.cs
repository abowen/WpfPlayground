using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace WpfPlayground.ViewModels
{
    public class DelegateViewModel : PropertyChangedBase
    {

        #region Delegate

        private int _delegateNumber;

        public int DelegateNumber
        {
            get { return _delegateNumber; }
            set
            {
                _delegateNumber = value;
                NotifyOfPropertyChange(() => DelegateNumber);
            }
        }

        public delegate int DelegateHandler(int input);

        public void DelegateButton()
        {
            var handler = new DelegateHandler(DelegateHandlerImplementation);
            DelegateNumber = handler(DelegateNumber);
        }

        public int DelegateHandlerImplementation(int input)
        {
            return ++input;
        }

        #endregion


        #region Event

        // AB: Easier to use over delegates for multiple subscribers & complex method signatures 

        private int _eventNumber;

        public int EventNumber
        {
            get { return _eventNumber; }
            set
            {
                _eventNumber = value;
                NotifyOfPropertyChange(() => EventNumber);
            }
        }

        // Don't need to create custom Event Args
        public delegate int MyEventHandler(object sender, MyEventArgs eventArgs);

        public event MyEventHandler EventHandler;

        public void EventButton()
        {
            if (EventHandler == null)
            {
                // AB: Can be explicit or let the compiler match the signature
                //EventHandler += new EventHandler(OnEventHandler);
                EventHandler += OnEventHandler;
            }
            if (EventHandler != null)
            {
                EventNumber = EventHandler(this, new MyEventArgs() { SomeValue = EventNumber });
            }

        }

        private int OnEventHandler(object sender, MyEventArgs eventArgs)
        {
            var value = ++eventArgs.SomeValue;
            return value;
        }


        #endregion


        #region Generic Event

        // AB: Easier to use than basic event as no need to explicitly define the delegate by reusing the built in EventHandler delegate

        private int _eventGenericNumber;

        public int EventGenericNumber
        {
            get { return _eventGenericNumber; }
            set
            {
                _eventGenericNumber = value;
                NotifyOfPropertyChange(() => EventGenericNumber);
            }
        }

        public event EventHandler<MyEventArgs> EventGenericHandler;

        public void EventGenericButton()
        {
            if (EventGenericHandler == null)
            {
                // AB: Can be explicit or let the compiler use inference on the signature
                //EventHandler += new EventHandler(OnEventHandler);
                EventGenericHandler += OnEventGenericHandler;
            }
            if (EventGenericHandler != null)
            {
                EventGenericHandler(this, new MyEventArgs() { SomeValue = 1 });
            }

        }

        private void OnEventGenericHandler(object sender, MyEventArgs eventArgs)
        {
            EventGenericNumber += eventArgs.SomeValue;
        }

        #endregion

        #region Anonymous Delegate

        // Allows you to write the handlers inline instead of separate method call

        private int _delegateAnonymousNumber;

        public int DelegateAnonymousNumber
        {
            get { return _delegateAnonymousNumber; }
            set
            {
                _delegateAnonymousNumber = value;
                NotifyOfPropertyChange(() => DelegateAnonymousNumber);
            }
        }

        public event EventHandler<MyEventArgs> DelegateAnonymousHandler;

        public void DelegateAnonymousButton()
        {

            if (DelegateAnonymousHandler == null)
            {
                DelegateAnonymousHandler += delegate(object sender, MyEventArgs args)
                {
                    DelegateAnonymousNumber += args.SomeValue;
                };
            }

            DelegateAnonymousHandler(this, new MyEventArgs() { SomeValue = 1 });
        }

        #endregion

        #region Lambda Delegate

        // Allows you to write event shorter version of anonymous delegate

        private int _delegateLambdaNumber;

        public int DelegateLambdaNumber
        {
            get { return _delegateLambdaNumber; }
            set
            {
                _delegateLambdaNumber = value;
                NotifyOfPropertyChange(() => DelegateLambdaNumber);
            }
        }

        public event EventHandler<MyEventArgs> DelegateLambdaHandler;

        public void DelegateLambdaButton()
        {

            if (DelegateLambdaHandler == null)
            {
                DelegateLambdaHandler += (s, e) =>
                {
                    DelegateLambdaNumber += e.SomeValue;
                };

            }

            DelegateLambdaHandler(this, new MyEventArgs() { SomeValue = 1 });
        }

        #endregion

        #region Action

        // Shorter version of events where you only have on listener and no need to write custom EventArgs

        private int _actionNumber;

        public int ActionNumber
        {
            get { return _actionNumber; }
            set
            {
                _actionNumber = value;
                NotifyOfPropertyChange(() => ActionNumber);
            }
        }

        public Action<int> Action;

        // AB: Minor issue with calling it ActionButton
        public void MyActionButton()
        {
            if (Action == null)
            {
                Action = i => ActionNumber = ActionNumber + i;
            }

            Action(1);
        }

        #endregion

        #region Function

        // Similar to function but returns a value

        private int _functionNumber;

        public int FunctionNumber
        {
            get { return _functionNumber; }
            set
            {
                _functionNumber = value;
                NotifyOfPropertyChange(() => FunctionNumber);
            }
        }

        public Func<int, int> Function;

        public void FunctionButton()
        {
            if (Function == null)
            {
                Function = i => FunctionNumber + i;
            }

            FunctionNumber = Function(1);
        }

        #endregion

    }

    public class MyEventArgs : EventArgs
    {
        public int SomeValue { get; set; }
    }
}
