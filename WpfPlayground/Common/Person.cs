using System.Windows.Media;

namespace WpfPlayground.Common
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string JobTitle { get; set; }
        public bool IsAvailable { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public Color FavouriteColor { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
