
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


namespace Feedr
{
	[Activity (Label = "AppPost")]			
	public class AddPost : Activity
	{

		ParseHandler objParse = ParseHandler.Default;
		ImageView PostProfilePic;
		TextView PostUsrName;
		TextView PostDate;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AddPost);

			PostProfilePic = FindViewById<ImageView> (Resource.Id.PostProfilePic);
			PostUsrName = FindViewById<TextView> (Resource.Id.PostUsrName);
			PostDate = FindViewById<TextView> (Resource.Id.PostDate);

			LoadUserDetails ();

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

