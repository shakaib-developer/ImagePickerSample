using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImagePickerSample.Utility;
using ImagePickerSample.WebServices;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ImagePickerSample.ViewModels
{
    public class UploadImageViewModel:BaseViewModel
    {
        INavigation navigation;

        bool _IsLoading;
        public bool IsLoading { get { return _IsLoading; } set { _IsLoading = value; OnPropertyChanged(); } }

        string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { if (value != null) _ImagePath = value; OnPropertyChanged(); }
        }

        public UploadImageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        private async void Browse()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                ToastClass.ShowToast("Photos Permission Required");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 400,
            });
            //if(file.)

            if (file == null)
                return;

            var stream = file.GetStream();
            if (stream != null)
            {
                string fileExtension = file.Path.Split('.').LastOrDefault();
                var StreamByte = ReadAllBytes(stream);
                await UploadDocument(StreamByte, "image." + fileExtension);
            }
        }
        private async void Capture()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                ToastClass.ShowToast("No Camera Available");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Images Folder",
                SaveToAlbum = true,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 400,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            var stream = file.GetStream();
            if (stream != null)
            {
                string fileExtension = file.Path.Split('.').LastOrDefault();
                var StreamByte = ReadAllBytes(stream);

                await UploadDocument(StreamByte, "image." + fileExtension); ;
            }
        }

        private async Task UploadDocument(byte[] StreamByte, string fileExtension)
        {
            IsLoading = true;

            ApiCalls api = new ApiCalls();
            string res = await api.UpdateDP(StreamByte, fileExtension);

            if ((!string.IsNullOrEmpty(res)) || (!string.IsNullOrWhiteSpace(res)))
            {

                ToastClass.ShowToast("Profile picture updated succesfully");

                ImagePath = res;
            }

            IsLoading = false;
        }

        public byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public Command ChangeDpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var res = await Application.Current.MainPage.DisplayActionSheet("Select Image", "Cancel", "Browse", "Capture");

                    if (res == null || res == "Cancel")
                        return;
                    else if (res.Equals("Browse"))
                        Browse();
                    else if (res.Equals("Capture"))
                        Capture();
                });
            }
        }
    }
}
