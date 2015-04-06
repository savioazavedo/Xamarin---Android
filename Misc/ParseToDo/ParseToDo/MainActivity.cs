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


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnLogin = FindViewById<Button> (Resource.Id.btnLogin);
			btnRegister = FindViewById<Button> (Resource.Id.btnLinkToRegister);

			btnLogin.Click += LoginClick;
			btnRegister.Click += RegisterClick;

		}

		void RegisterClick (object sender, EventArgs e)
		{
			StartActivity(typeof(RegisterUser));
		}

		void LoginClick (object sender, EventArgs e)
		{

		}
	}
}


