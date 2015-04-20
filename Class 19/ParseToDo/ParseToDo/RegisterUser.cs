
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
using System.Threading.Tasks;
using Parse;

namespace ParseToDo
{
	[Activity (Label = "RegisterUser")]			
	public class RegisterUser : Activity
	{
		EditText txtUserName;
		EditText txtEmail;
		EditText txtPassword;
		Button btnRegister;
		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Register);

			txtUserName = FindViewById<EditText>(Resource.Id.txtUserName);
			txtEmail = FindViewById<EditText> (Resource.Id.txtEmail);
			txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);
			btnRegister = FindViewById<Button> (Resource.Id.btnRegister);
	
			btnRegister.Click += OnRegisterButtonClick;
		}

		void OnRegisterButtonClick (object sender, EventArgs e)
		{
			RegisterNewUser();
		}


		private async void RegisterNewUser()
		{
			Toast.MakeText (this, "Please wait ...", ToastLength.Short).Show ();

			var result = await objParse.CheckIfUserNameExists (txtUserName.Text);

			if (result == true) {
				Toast.MakeText (this, "Username already exists", ToastLength.Long).Show ();
			} else {
				await objParse.CreateUserAsync(txtUserName.Text,txtEmail.Text,txtPassword.Text);
				Toast.MakeText (this, "Account Successfully Created ", ToastLength.Short).Show ();
				Toast.MakeText (this, "Please Login Again", ToastLength.Short).Show ();
				ClearAll ();
				StartActivity (typeof(MainActivity));
			}
		}

		void ClearAll()
		{
			txtUserName.Text = "";
			txtPassword.Text = "";
			txtEmail.Text = "";
		}
			
	}
}

