using System.Collections.Generic;
using System.Windows.Media;
using Caliburn.Micro;
using WpfPlayground.Common;

namespace WpfPlayground.ViewModels
{
    public class DataTemplateViewModel : PropertyChangedBase
    {
        public DataTemplateViewModel()
        {
            People = new List<Person>
            {
                new Person
                {
                    FirstName = "Andrew",
                    LastName = "Bowen",
                    FavouriteColor = Colors.DarkRed,
                    JobTitle = "Software Engineer",
                    IsAvailable = true,
                    City = "Sydney",
                    Email = "Some01@Email.com"
                },
                new Person
                {
                    FirstName = "James",
                    LastName = "Smith",
                    FavouriteColor = Colors.Purple,
                    JobTitle = "Waiter",
                    IsAvailable = false,
                    City = "Sydney",
                    Email = "Some02@Email.com"
                },
                new Person
                {
                    FirstName = "Lisa",
                    LastName = "Brown",
                    FavouriteColor = Colors.Peru,
                    JobTitle = "Doctor",
                    IsAvailable = true,
                    City = "Canberra",
                    Email = "Some03@Email.com"
                },
                new Person
                {
                    FirstName = "Michelle",
                    LastName = "Taylor",
                    FavouriteColor = Colors.CornflowerBlue,
                    JobTitle = "Nurse",
                    IsAvailable = false,
                    City = "Melbourne",
                    Email = "Some04@Email.com"
                }
            };
        }

        public IEnumerable<Person> People { get; private set; }
    }
}
