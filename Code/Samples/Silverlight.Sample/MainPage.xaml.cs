namespace Silverlight.Sample
{
    using System;
    using System.Windows;

    public partial class MainPage
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
