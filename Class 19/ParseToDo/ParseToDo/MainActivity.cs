using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ParseToDo
{
	[Activity (Label = "ParseToDo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		Button btnLogin;
		Button btnRegister;
		EditText txtUsername;
		EditText txtPassword;
		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
			btnRegister = FindViewById<Button> (Resource.Id.btnLinkToRegister);
			txtUsername = FindViewById<EditText> (Resource.Id.username);
			txtPassword = FindViewById<EditText> (Resource.Id.password);

			btnLogin.Click += LoginClick;
			btnRegister.Click += RegisterClick;

		}

		void RegisterClick (object sender, EventArgs e)
		{
			StartActivity(typeof(RegisterUser));
		}

		public async void LoginClick (object sender, EventArgs e)
		{
			if(txtUsername.Text != "" && txtPassword.Text != "")
			{
				var result = await objParse.Login (txtUsername.Text, txtPassword.Text);

				if (result == true)
				{
					Toast.MakeText (this, "Login Successful", ToastLength.Long).Show ();
					StartActivity (typeof(ViewList));
				} else {
					Toast.MakeText (this, "Login Unsuccessful. Please check your username and password", ToastLength.Long).Show ();
				}
			}
		}
	}
}


