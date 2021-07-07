using System;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace ImagePickerSample.Utility
{
    public static class ToastClass
    {
        public static void ShowToast(string mtitle = "", string msg = "", int durationMilliSeconds = 4000)
        {
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Font = Font.SystemFontOfSize(16),
                Message = msg
            };
            var options = new ToastOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(durationMilliSeconds),
                BackgroundColor = Color.Default,
                IsRtl = false
            };

            if (mtitle == "W")
            {
                //imgAlert.Source = "warning.png";
                options.BackgroundColor = Color.FromHex("#FFFEF2");
                messageOptions.Foreground = Color.FromHex("#FFBC33");
            }
            else if (mtitle == "S")
            {
                //imgAlert.Source = "tick.png";
                options.BackgroundColor = Color.FromHex("#EDFFED");
                messageOptions.Foreground = Color.FromHex("#6AC259");
            }
            else if (mtitle == "E")
            {
                //imgAlert.Source = "crossRed.png";
                options.BackgroundColor = Color.FromHex("#F2DEDE");
                messageOptions.Foreground = Color.FromHex("#E24C4B");
            }
            else
            {
                //imgAlert.Source = "info.png";
                options.BackgroundColor = Color.FromHex("#D9EDF7");
                messageOptions.Foreground = Color.Blue;
            }

            Application.Current.MainPage.DisplayToastAsync(options);
        }
    }
}
