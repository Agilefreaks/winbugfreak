using System;
using System.Windows;
using System.Windows.Controls;

namespace Silverlight.Sample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new Exception("Boom!");
        }
    }
}