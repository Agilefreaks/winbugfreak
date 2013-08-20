using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WinRT.Sample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CallToSetStackTrace();
        }

        private void CallToSetStackTrace()
        {
            Boom();
        }

        private void Boom()
        {
            throw new Exception("Boom!");
        }
    }
}
