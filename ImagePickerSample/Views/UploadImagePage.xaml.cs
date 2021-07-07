using System;
using System.Collections.Generic;
using ImagePickerSample.ViewModels;
using Xamarin.Forms;

namespace ImagePickerSample.Views
{
    public partial class UploadImagePage : ContentPage
    {
        public UploadImagePage()
        {
            InitializeComponent();

            BindingContext = new UploadImageViewModel(Navigation);
        }
    }
}
