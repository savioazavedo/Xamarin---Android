
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

		}


	}
}

