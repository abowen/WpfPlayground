using System;
using System.Collections.Generic;
using System.Windows.Media;
using Caliburn.Micro;
using WpfPlayground.Helper;

namespace WpfPlayground.ViewModels
{
    public class ThemeViewModel : Screen
    {
        public ThemeViewModel()
        {
            _random = new Random();
            _knownColors = ColorHelper.GetKnownColors();            
            RandomiseColor();
        }
        
        private readonly Random _random;
        private readonly List<KeyValuePair<string, Color>> _knownColors;               

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

        public void RandomiseColourButton()
        {
            RandomiseColor();
        }

        void RandomiseColor()
        {
            var index = _random.Next(_knownColors.Count);
            RandomColour = _knownColors[index].Value;
        }
    }
}
