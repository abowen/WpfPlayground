using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfPlayground.Views
{
    /// <summary>
    /// Interaction logic for AnimationView.xaml
    /// </summary>
    public partial class AnimationView : UserControl
    {
        public AnimationView()
        {
            InitializeComponent();
        }

        private void CodeBehindStackPanel_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = 100,
                To = 30,
                Duration = new Duration(TimeSpan.Parse("0:0:2")),
                AutoReverse = true
            };
            CodeBehindStackPanel.BeginAnimation(FrameworkElement.WidthProperty, animation);
        }
    }
}
