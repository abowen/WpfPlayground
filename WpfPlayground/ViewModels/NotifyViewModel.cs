using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using WpfPlayground.Annotations;

namespace WpfPlayground.ViewModels
{
    public class NotifyViewModel : PropertyChangedBase
    {
        public NotifyViewModel()
        {
            People = new ObservableCollection<IPerson>();
            Amount = 1000;
        }

        private ObservableCollection<IPerson> _people;
        public ObservableCollection<IPerson> People
        {
            get { return _people; }
            set { _people = value; NotifyOfPropertyChange(() => People); }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }

        private long _createTiming;
        public long CreateTiming
        {
            get { return _createTiming; }
            set { _createTiming = value; NotifyOfPropertyChange(() => CreateTiming); }
        }

        private long _updateTiming;
        public long UpdateTiming
        {
            get { return _updateTiming; }
            set { _updateTiming = value; NotifyOfPropertyChange(() => UpdateTiming); }
        }

        private long _dateTiming;
        public long DateTiming
        {
            get { return _dateTiming; }
            set { _dateTiming = value; NotifyOfPropertyChange(() => DateTiming); }
        }

        public int TestProperty;

        public void PersonAButton()
        {
            using (new MyTimerDateTime(ms => DateTiming = ms))
            {
                using (new MyTimer(ms => CreateTiming = ms))
                {
                    People = new ObservableCollection<IPerson>(PersonFactory<PersonA>.CreateList(Amount));
                }

                using (new MyTimer(ms => UpdateTiming = ms))
                {
                    foreach (PersonA person in People)
                    {
                        person.OnPropertyChangedDebug("TestProperty");
                    }
                }
            }
        }

        public void PersonBButton()
        {
            using (new MyTimerDateTime(ms => DateTiming = ms))
            {
                using (new MyTimer(ms => CreateTiming = ms))
                {
                    People = new ObservableCollection<IPerson>(PersonFactory<PersonB>.CreateList(Amount));
                }
                using (new MyTimer(ms => UpdateTiming = ms))
                {
                    foreach (PersonB person in People)
                    {
                        person.NotifyExpressionDebug(() => TestProperty);
                    }
                }
            }
        }

        public void PersonCButton()
        {
            using (new MyTimerDateTime(ms => DateTiming = ms))
            {
                using (new MyTimer(ms => CreateTiming = ms))
                {
                    People = new ObservableCollection<IPerson>(PersonFactory<PersonC>.CreateList(Amount));
                }
                using (new MyTimer(ms => UpdateTiming = ms))
                {
                    foreach (PersonC person in People)
                    {
                        person.OnPropertyChangedDebug();
                    }
                }
            }
        }

        public void UpdateButton()
        {
            using (new MyTimerDateTime(ms => DateTiming = ms))
            {
                using (new MyTimer(ms => UpdateTiming = ms))
                {
                    foreach (var person in People)
                    {
                        person.Age++;
                        person.Name += "+";
                    }
                }
            }
        }
    }

    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
    }

    public static class PersonFactory<T> where T : IPerson, new()
    {
        public static IEnumerable<IPerson> CreateList(int number)
        {
            for (var i = 0; i < number; i++)
            {
                var person = new T
                {
                    Age = i,
                    Name = "Name " + i
                };
                yield return person;
            }
        }
    }


    /// <summary>
    /// String
    /// </summary> 
    public class PersonA : INotifyPropertyChanged, IPerson
    {
        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        public virtual void OnPropertyChangedDebug(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Expression
    /// - CookBook pre C# 5.0
    /// - Caliburn Micro (similar)
    /// </summary>
    public class PersonB : INotifyPropertyChanged, IPerson
    {
        private int _age;
        public int Age
        {
            get { return _age; }
            set { SetProperty(ref _age, value, () => Age); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value, () => Name); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T field, T value, Expression<Func<T>> expr)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var lambda = (LambdaExpression)expr;
                MemberExpression memberExpr;
                if (lambda.Body is UnaryExpression)
                {
                    var unaryExpr = (UnaryExpression)lambda.Body;
                    memberExpr = (MemberExpression)unaryExpr.Operand;
                }
                else
                {
                    memberExpr = (MemberExpression)lambda.Body;
                }
                OnPropertyChanged(memberExpr.Member.Name);
            }
        }

        [Conditional("DEBUG")]
        public void NotifyExpressionDebug<T>(Expression<Func<T>> expr)
        {
            var lambda = (LambdaExpression)expr;
            MemberExpression memberExpr;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpr = (UnaryExpression)lambda.Body;
                memberExpr = (MemberExpression)unaryExpr.Operand;
            }
            else
            {
                memberExpr = (MemberExpression)lambda.Body;
            }
            OnPropertyChanged(memberExpr.Member.Name);
        }
    }

    /// <summary>
    /// CallerMemberName
    /// - Resharpers C# auto-implemented
    /// - Pluralsight C# Tips n Tricks
    /// - Cookbook C# 5.0
    /// </summary>
    public class PersonC : INotifyPropertyChanged, IPerson
    {
        private int _age;

        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged(); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [Conditional("DEBUG")]
        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChangedDebug([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class MyTimer : IDisposable
    {
        private readonly Action<long> _updateItem;
        //private readonly TextBlock _textBlock;
        private Stopwatch _stopwatch;

        public MyTimer(Action<long> updateItem)
        {
            _updateItem = updateItem;
            //_textBlock = textBlock;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            if (_stopwatch == null) return;

            _stopwatch.Stop();
            _updateItem(_stopwatch.ElapsedMilliseconds);
            //_textBlock.Text = string.Format("{0}ms", _stopwatch.ElapsedMilliseconds);
            _stopwatch = null;
        }
    }

    public class MyTimerDateTime : IDisposable
    {
        private readonly Action<long> _updateItem;
        //private readonly TextBlock _textBlock;
        private DateTime _startDateTime;

        public MyTimerDateTime(Action<long> updateItem)
        {
            _updateItem = updateItem;
            //_textBlock = textBlock;
            _startDateTime = DateTime.UtcNow;
        }

        public void Dispose()
        {
            var elapsedMilliseconds = Convert.ToInt64((DateTime.UtcNow - _startDateTime).TotalMilliseconds);
            _updateItem(elapsedMilliseconds);
        }
    }
}
