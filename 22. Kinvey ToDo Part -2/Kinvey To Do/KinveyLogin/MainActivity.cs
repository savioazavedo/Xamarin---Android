using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinAndroid;

namespace KinveyLogin
{
    [Activity(Label = "KinveyLogin", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class MainActivity : Activity
    {
        int count = 1;

        TextView txtUsername, txtPassword;


        private string appKey = "kid_VTNND_fxXq";
        private string appSecret = "ab5bebc75fa147c0885c074cdf48832c";
        Client kinveyClient;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button buttonRegister = FindViewById<Button>(Resource.Id.btnLinkToRegister);
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            txtUsername = FindViewById<TextView>(Resource.Id.username);
            txtPassword = FindViewById<TextView>(Resource.Id.password);

            buttonRegister.Click += ButtonRegister_Click; ;
            btnLogin.Click += BtnLogin_Click;


            kinveyClient = new Client.Builder(appKey, appSecret)
                .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
                .setOfflinePlatform(new SQLitePlatformAndroid())
                .setLogger(delegate (string msg) { Console.WriteLine(msg); })
                .build();

            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }

        }


        private async void BtnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                User myUser = await kinveyClient.User().LoginAsync(txtUsername.Text, txtPassword.Text);
                Toast.MakeText(this, "Login Successful", ToastLength.Short).Show();
                StartActivity(typeof(ToDoActivity));

            } catch(Exception ex)
            {
                Toast.MakeText(this,"Error:" + ex.Message,ToastLength.Short).Show();
            }
                
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(RegisterActivity));
        }
    }
}

