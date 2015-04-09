using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Feedr
{
	[Activity (Label = "Feedr", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		Button btnRegister;
		Button btnLogin;
		EditText txtUsername;
		EditText txtPassword;
		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			btnRegister = FindViewById<Button> (Resource.Id.btnLinkToRegister);
			btnRegister.Click += RegisterClick;

			btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
			btnLogin.Click += LoginClick;
			txtUsername = FindViewById<EditText> (Resource.Id.username);
			txtPassword = FindViewById<EditText> (Resource.Id.password);

		}

		public async void LoginClick (object sender, EventArgs e)
		{
			if(txtUsername.Text != "" && txtPassword.Text != "")
			{
				var result = await objParse.Login (txtUsername.Text, txtPassword.Text);

				if (result == true)
				{
					Toast.MakeText (this, "Login Successful", ToastLength.Long).Show ();
					StartActivity (typeof(AddPost));
				} else {
					Toast.MakeText (this, "Login Unsuccessful. Please check your username and password", ToastLength.Long).Show ();
				}
			}
		}


		void RegisterClick (object sender, EventArgs e)
		{
			StartActivity(typeof(RegisterUser));
		}

	}
}


