using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinAndroid;

namespace KinveyLogin
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {

        EditText txtUserName;
        EditText txtEmail;
        EditText txtPassword;
        Button btnRegister;

        private string appKey = "kid_VTNND_fxXq";
        private string appSecret = "ab5bebc75fa147c0885c074cdf48832c";
        Client kinveyClient;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.Register);

            txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            btnRegister.Click += OnRegisterButtonClick;

            kinveyClient = new Client.Builder(appKey, appSecret)
                .setFilePath(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))
                .setOfflinePlatform(new SQLitePlatformAndroid())
                .setLogger(delegate (string msg) { Console.WriteLine(msg); })
                .build();

        }

        protected override void OnDestroy()
        {
            if (kinveyClient.User().isUserLoggedIn())
            {
                kinveyClient.User().Logout();
            }
        }


        void OnRegisterButtonClick(object sender, EventArgs e)
        {
            RegisterNewUser();
        }

        private async void RegisterNewUser()
        {
            // Register New User 

            if(txtUserName.Text != "" && txtPassword.Text != "")
            {

                try
                {
                    User myUser = await kinveyClient.User().CreateAsync(txtUserName.Text, txtPassword.Text);
                  
                    if (kinveyClient.User().isUserLoggedIn())
                    {
                        Toast.MakeText(this, "Logging in", ToastLength.Long).Show();
                    } else
                    {
                        StartActivity(typeof(MainActivity));
                        Toast.MakeText(this, "User successfully created, Please Login", ToastLength.Long).Show();
                    }

                }
                catch (Exception ex)
                {
                    Toast.MakeText(this,"User already exists",ToastLength.Long).Show();
                }           
            }
        }




    }
}