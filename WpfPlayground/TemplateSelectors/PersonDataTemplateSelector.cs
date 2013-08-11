using System.Windows;
using System.Windows.Controls;
using WpfPlayground.Common;

namespace WpfPlayground.TemplateSelectors
{
    public class PersonDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item != null && item is Person)
            {
                var person = item as Person;

                if (person.IsAvailable)
                {
                    return
                        element.FindResource("PersonIsAvailableDataTemplate") as DataTemplate;
                }

                return element.FindResource("PersonIsNotAvailableDataTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
