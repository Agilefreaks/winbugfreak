namespace Wpf.Sample
{
    using System;

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            throw new Exception("Boom!");
        }
    }
}
