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

        public new string DisplayName
        {
            get { return "Themes"; } set { throw new NotImplementedException(); }
        }

        private readonly Random _random;
        private readonly List<KeyValuePair<string, Color>> _knownColors;
        

        public void RandomiseColourButton()
        {
            RandomiseColor();
        }

        void RandomiseColor()
        {
            var index = _random.Next(_knownColors.Count);
            RandomColour = _knownColors[index].Value;
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
    }
}
