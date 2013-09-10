namespace Wpf.Sample
{
    using System;
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BoomClick(object sender, RoutedEventArgs e)
        {
            throw new Exception("Boom!");
        }
    }
}
