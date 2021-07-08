using System;
using ImagePickerSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImagePickerSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new UploadImagePage());
        }

        public void DisplayThePDF(string url)
        {
            MainPage = new NavigationPage(new UploadImagePage(url));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
