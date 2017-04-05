using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;
using System.Net;
using Android.Graphics;

namespace FacebookOAuth
{
    [Activity(Label = "FacebookOAuth", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        TextView txtName;
        TextView txtEmail;
        ImageView fbPic;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            txtEmail = FindViewById<TextView>(Resource.Id.Email);
            txtName = FindViewById<TextView>(Resource.Id.Name);
            fbPic = FindViewById<ImageView>(Resource.Id.fbPic);


            button.Click += Facebook_Login ;
        }

        private void Facebook_Login(object sender, EventArgs e)
        {
            // If authorization succeeds or is canceled, .Completed will be fired.

            try
            {
                var auth = new OAuth2Authenticator(
                    clientId: "1128857693908081",
                    scope: "email",
                    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                    redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

                auth.Completed += LoginComplete;

                var intent = auth.GetUI(this);

                StartActivity(intent);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public async void LoginComplete(object sender, AuthenticatorCompletedEventArgs e)
        {
            // We presented the UI, so it's up to us to dismiss it.
            //DismissViewController (true, null);

            if (!e.IsAuthenticated)
            {
                Toast.MakeText(this, "Not Authenticated", ToastLength.Short).Show();
                return;
            }

            Toast.MakeText(this, "Authenticated", ToastLength.Short).Show();

            //// Now that we're logged in, make a OAuth2 request to get the user's id.
            var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=email,name,picture{url}"), null, e.Account);
            var response = await request.GetResponseAsync();

            var obj = Newtonsoft.Json.Linq.JObject.Parse(response.GetResponseText());

            Console.WriteLine(obj.ToString());

            txtEmail.Text = obj["email"].ToString();
            txtName.Text = obj["name"].ToString();

            String PicURL = obj["picture"].SelectToken("data.url").ToString();

            var imageBitmap = GetImageBitmapFromUrl(PicURL);
            fbPic.SetImageBitmap(imageBitmap);
          
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            if (!(url == "null"))
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

            return imageBitmap;
        }


    }
}

