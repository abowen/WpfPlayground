using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using FluentValidation;
using FluentValidation.Results;

namespace WpfPlayground.ViewModels
{
    public class ValidationViewModel : PropertyChangedBase
    {
        public ValidationViewModel()
        {
            MyClass = new MyClass();
        }

        private MyClass _myClass;
        public MyClass MyClass
        {
            get { return _myClass; }
            set
            {
                _myClass = value;
                NotifyOfPropertyChange(() => MyClass);
            }
        }

        private IEnumerable<string> _errors = new string[0];
        public IEnumerable<string> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;
                NotifyOfPropertyChange(() => Errors);
            }
        }


        public void ValidationButton()
        {

            var validator = new MyClassValidator();
            var results = validator.Validate(MyClass);

            var validationSucceeded = results.IsValid;
            Errors = results.Errors.Select(e => e.ErrorMessage);
        }


    }

    public class MyClass : PropertyChangedBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyOfPropertyChange(() => Name); }
        }
    }

    public class MyClassValidator : AbstractValidator<MyClass>
    {
        public MyClassValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty();
        }
    }
}
