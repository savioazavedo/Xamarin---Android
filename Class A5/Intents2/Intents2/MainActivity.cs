using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Intents2
{
	[Activity (Label = "Intents2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		EditText PhoneNumber;
		EditText Latitude;
		EditText Longitude;

		Button btnDial;
		Button btnOpenMap;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//Initialize Controls

			PhoneNumber = FindViewById<EditText> (Resource.Id.txtPhoneNumber);
			Latitude = FindViewById<EditText> (Resource.Id.txtLat);
			Longitude = FindViewById<EditText> (Resource.Id.txtlong);

			btnDial = FindViewById<Button> (Resource.Id.btnCall);
			btnOpenMap = FindViewById<Button> (Resource.Id.btnOpenMap);

			// Attach Events 
			btnDial.Click += OnDialButtonClick;
			btnOpenMap.Click += OnOpenMapClick;
		}

		public void OnDialButtonClick(object sender,EventArgs e)
		{
			var uri = Android.Net.Uri.Parse ("tel:" + PhoneNumber.Text);
			var intent = new Intent (Intent.ActionView, uri); 
			StartActivity (intent);
		}

		public void OnOpenMapClick(object sender,EventArgs e)
		{
			var geoUri = Android.Net.Uri.Parse ("geo:" + Latitude.Text + "," + Longitude.Text );
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			StartActivity (mapIntent);
		}
			
	}
}


