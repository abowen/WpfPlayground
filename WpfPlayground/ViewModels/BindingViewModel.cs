﻿using Caliburn.Micro;
using System.Windows.Media;
using WpfPlayground.Helpers;

namespace WpfPlayground.ViewModels
{
    public class BindingViewModel : Screen
    {
        public BindingViewModel()
        {
            RandomiseColourButton();
            RandomDecimal = 12.3m;
        }
           
        private Color _randomColour;

        public Color RandomColour
        {
            get { return _randomColour; }
            set
            {
                _randomColour = value;
                NotifyOfPropertyChange(() => RandomColour);
            }
        }

        public decimal RandomDecimal { get; set; }    

        public void RandomiseColourButton()
        {
            RandomColour = ColorHelper.GetRandomColor();
        }
    }
}
