using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using ImagePickerSample.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ImagePickerSample.WebServices
{
    public class ApiCalls
    {
        #region Check Permissions
        public static bool IsInternetConnected()
        {
            var profiles = Connectivity.ConnectionProfiles;
            if (profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Cellular))
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {
                    return true;
                }
                else
                {
                    ToastClass.ShowToast("W", "Network unavailable");
                    return false;
                }
            }
            else
            {
                ToastClass.ShowToast("E", "No internet connection");
                return false;
            }
        }
        #endregion


        #region Update DP ApiCall --
        public async System.Threading.Tasks.Task<string> UpdateDP(byte[] dp, string fileName)
        {
            string res = "";
            HttpStatusCode responseStatusCode = 0;

            if (IsInternetConnected())
            {
                try
                {
                    var client = new RestClient("https://heart-social.it/api/sharedemo.php");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("secret_key", "PybRB1lcbBO5qVxdhcQpPLHTnAwAadTfs");
                    request.AddHeader("issuer_claim", "6A151242EC67AD17s");
                    request.AddHeader("audience_claim", "9561Cs");
                    request.AddParameter("email", "");
                    request.AddParameter("authcode", "");
                    request.AddParameter("codtype", "");
                    request.AddParameter("title", "");
                    request.AddParameter("description", "");
                    request.AddFile("file", dp, fileName, "image/jpeg");
                    IRestResponse response = await client.ExecuteAsync(request);
                    Console.WriteLine(response.Content);

                    //////////////////////////////////////////////////////////////////////////

                    //IsBusy = true;
                    //var Httpclient = new HttpClient();

                    //Httpclient.DefaultRequestHeaders.Add("secret_key", "PybRB1lcbBO5qVxdhcQpPLHTnAwAadTfs");
                    //Httpclient.DefaultRequestHeaders.Add("issuer_claim", "6A151242EC67AD17s");
                    //Httpclient.DefaultRequestHeaders.Add("audience_claim", "9561Cs");

                    //var url = "https://heart-social.it/api/sharedemo.php";

                    //var uri = new Uri(string.Format(url, string.Empty));

                    //var json = JsonConvert.SerializeObject(new { email = "", authcode = "", codtype = "", title = "", description = "", file = dp });
                    //var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //HttpResponseMessage response = null;

                    //response = await Httpclient.PostAsync(uri, content);

                    responseStatusCode = response.StatusCode;

                    if (responseStatusCode == HttpStatusCode.OK)
                    {

                        var responseContent = response.Content;

                        var jObject = JObject.Parse(responseContent);
                        string message = (string)jObject.GetValue("message");
                        
                        if (message != "OK")
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                ToastClass.ShowToast("E", message);
                            });

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                ToastClass.ShowToast("S", "Image uploaded successfully");
                            });
                        }
                    }
                    else
                    {
                        ToastClass.ShowToast("E", "Something went wrong.");
                    }
                }
                catch (Exception)
                {
                    res = "";
                    ToastClass.ShowToast("E", "Something went wrong");
                }
            }

            return res;

        }
        #endregion
    }
}
