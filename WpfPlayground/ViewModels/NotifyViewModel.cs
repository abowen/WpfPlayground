using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using WpfPlayground.Annotations;
using System.Linq.Expressions;

namespace WpfPlayground.ViewModels
{
    public class NotifyViewModel : PropertyChangedBase
    {
        public NotifyViewModel()
        {
            People = new ObservableCollection<IPerson>();
        }

        private readonly int _listCount = 1000;

        private ObservableCollection<IPerson> _people;
        public ObservableCollection<IPerson> People
        {
            get { return _people; }
            set { _people = value; NotifyOfPropertyChange(() => People); }
        }

        private long _elapsedTime;
        public long ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                NotifyOfPropertyChange(() => ElapsedTime);
            }
        }

        public int TestProperty;

        public void PersonAButton()
        {
            People = new ObservableCollection<IPerson>(PersonFactory<PersonA>.CreateList(_listCount));

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (PersonA person in People)
            {
                person.OnPropertyChangedDebug("TestProperty");
            }
            stopWatch.Stop();
            ElapsedTime = stopWatch.ElapsedMilliseconds;
        }

        public void PersonBButton()
        {
            People = new ObservableCollection<IPerson>(PersonFactory<PersonB>.CreateList(_listCount));

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (PersonB person in People)
            {
                person.NotifyExpressionDebug(() => TestProperty);
            }
            stopWatch.Stop();
            ElapsedTime = stopWatch.ElapsedMilliseconds;
        }

        public void PersonCButton()
        {
            People = new ObservableCollection<IPerson>(PersonFactory<PersonC>.CreateList(_listCount));

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (PersonC person in People)
            {
                person.OnPropertyChangedDebug();
            }
            stopWatch.Stop();
            ElapsedTime = stopWatch.ElapsedMilliseconds;
        }

        public void IncrementButton()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var person in People)
            {
                person.Age++;
                person.Name += "+";
            }
            stopWatch.Stop();
            ElapsedTime = stopWatch.ElapsedMilliseconds;            
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
}
