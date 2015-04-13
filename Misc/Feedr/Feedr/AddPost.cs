
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
using Parse;
using Android.Graphics;
using System.Net;
using Android.Graphics.Drawables;
using System.IO;


namespace Feedr
{
	[Activity (Label = "AppPost")]			
	public class AddPost : Activity
	{

		ParseHandler objParse = ParseHandler.Default;
		ImageView PostProfilePic;
		TextView PostUsrName;
		TextView PostDate;
		ImageButton btnUpload;
		Button btnPost;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AddPost);

			PostProfilePic = FindViewById<ImageView> (Resource.Id.PostProfilePic);
			PostUsrName = FindViewById<TextView> (Resource.Id.PostUsrName);
			PostDate = FindViewById<TextView> (Resource.Id.PostDate);

			btnUpload = FindViewById<ImageButton> (Resource.Id.BtnUploadPostImage);
			btnUpload.Click += OnUploadClick;

			btnPost = FindViewById<Button> (Resource.Id.btnPost);
			btnPost.Click += OnPostClick;

			LoadUserDetails ();

		}

		void OnUploadClick (object sender, EventArgs e)
		{
			var imageIntent = new Intent ();
			imageIntent.SetType ("image/jpeg");
			imageIntent.SetAction (Intent.ActionGetContent);
			StartActivityForResult (
			Intent.CreateChooser (imageIntent, "Select photo"), 0);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if ((resultCode == Result.Ok) && (data != null))
			{

				btnUpload.SetScaleType (ImageView.ScaleType.FitXy);
				btnUpload.SetImageURI(data.Data);
			}
		}


		void OnPostClick (object sender, EventArgs e)
		{



		}

		public byte[] GetProfilePicInBytes()
		{
			var fetchedDrawable = btnUpload.Drawable;
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



		void LoadUserDetails()
		{
			PostUsrName.Text = objParse.GetCurrentUserInstance ().Username;
			PostDate.Text = DateTime.Now.ToString ();
			var pic = objParse.GetCurrentUserInstance ().Get<ParseFile> ("ProfilePic");
			PostProfilePic.SetImageBitmap(GetImageBitmapFromUrl(pic.Url.AbsoluteUri));
		}

		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;
			if(!(url=="null"))
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

