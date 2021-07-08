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

        public UploadImagePage(string url)
        {
            InitializeComponent();

            UploadImageViewModel vm = new UploadImageViewModel(Navigation);

            vm.ImagePath = url;

            BindingContext = vm;
        }
    }
}
