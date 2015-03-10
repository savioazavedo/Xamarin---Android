using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ImageView
{
	[Activity (Label = "ImageView", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
	
		ImageView imgCity; 

		Button btnAuckland;
		Button btnWellington;
		Button btnChristchurch;
		Button btnHamilton;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnAuckland = FindViewById<Button> (Resource.Id.btnAuckland);
			btnWellington = FindViewById<Button> (Resource.Id.btnWellington);
			btnChristchurch = FindViewById<Button> (Resource.Id.btnChristchurch);
			btnHamilton = FindViewById<Button> (Resource.Id.btnHamilton);

			var imgCity = FindViewById<ImageView> (Resource.Id.imgCity);



		}
	}
}


