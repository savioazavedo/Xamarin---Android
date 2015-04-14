
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
using Android.Graphics.Drawables;
using System.IO;
using Android.Graphics;


namespace Feedr
{
	[Activity (Label = "RegisterUser")]			
	public class RegisterUser : Activity
	{
		EditText txtUserName;
		EditText txtEmail;
		EditText txtPassword;
		Button btnRegister;
		ImageButton btnProfilePic;
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
			btnProfilePic = FindViewById<ImageButton> (Resource.Id.btnProfilePic);
	
			btnRegister.Click += OnRegisterButtonClick;
			btnProfilePic.Click += OnProfileButtonClick;
		}

		void OnProfileButtonClick (object sender, EventArgs e)
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/jpeg");
			imageIntent.SetAction (Intent.ActionGetContent);
			StartActivityForResult (Intent.CreateChooser (imageIntent, "Select photo"), 0);
		}


		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if ((resultCode == Result.Ok) && (data != null))
			{
				btnProfilePic.SetImageURI(data.Data);
			}
		}

		public byte[] GetProfilePicInBytes()
		{
			var fetchedDrawable = btnProfilePic.Drawable;
			BitmapDrawable bitmapDrawable = (BitmapDrawable)fetchedDrawable;
			var bitmap = bitmapDrawable.Bitmap;

			byte[] bitmapData;
			using (var stream = new MemoryStream())
			{
				bitmap.Compress(Bitmap.CompressFormat.Jpeg ,100, stream);
				bitmapData = stream.ToArray();
			}

			return bitmapData;
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
				await objParse.CreateUserAsync(txtUserName.Text,txtEmail.Text,txtPassword.Text,GetProfilePicInBytes());
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

