using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace BusLookATour
{
	[Activity (Label = "Bus Look-A-Tour", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button btnRoutes;
		Button btnUpdates;
		Button btnRouteMap;
		Button btnMahoe;
		Button btnContact;
		Button btnPlanner;
		Button btnFare;

		Button btnBusStop;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnFare = FindViewById <Button> (Resource.Id.btnFare);
			btnPlanner = FindViewById <Button> (Resource.Id.btnPlanner);
			btnRoutes = FindViewById <Button> (Resource.Id.btnRoutes);
			btnUpdates = FindViewById <Button> (Resource.Id.btnUpdates);
			btnRouteMap = FindViewById<Button> (Resource.Id.btnRouteMap);
			btnMahoe = FindViewById<Button> (Resource.Id.btnMahoe);
			btnContact = FindViewById <Button> (Resource.Id.btnContact);
			btnBusStop = FindViewById <Button> (Resource.Id.btnBusStop);

			btnFare.Click += OnFareBtnClick;
			btnPlanner.Click += OnPlannerBtnClick;
			btnRoutes.Click += OnRoutesBtnClick;
			btnUpdates.Click += OnUpdatesBtnClick;
			//btnMahoe.Click += OnBtnMahoeClick;
			btnContact.Click += OnBtnContactClick;
			btnBusStop.Click += OnBtnBusStopClick;

		}


		void OnBtnContactClick (object sender, EventArgs e)
		{
			StartActivity(typeof(ContactActivity));
		}

		void OnFareBtnClick (object sender, EventArgs e)
		{
			StartActivity(typeof(FareActivity));
		}

		void OnRoutesBtnClick (object sender, EventArgs e)
		{
			SetContentView (Resource.Layout.Routes);
		}

		void OnBtnBusStopClick (object sender, EventArgs e)
		{
			//StartActivity (typeof(MapActivity));
			var geoUri = Android.Net.Uri.Parse ("google.streetview:cbll=-37.795202,175.308452&cbp=1,90,,0,1.0&mz=20");
			var mapIntent = new Intent (Intent.ActionView, geoUri);
			StartActivity (mapIntent);
		}
			


//		void OnUpdatesBtnClick (object sender, EventArgs e)
//		{
//			SetContentView (Resource.Layout.Webview);
//
////			web_view = FindViewById<WebView> (Resource.Id.webview);
////			web_view.Settings.JavaScriptEnabled = true;
////			web_view.LoadUrl ("http://busit.co.nz/mobile/Service-updates/");
//		}

//		void OnPlannerBtnClick (object sender, EventArgs e)
//		{
//			SetContentView (Resource.Layout.Webview);
//
////			web_view = FindViewById<WebView> (Resource.Id.webview);
////			web_view.Settings.JavaScriptEnabled = true;
////			web_view.LoadUrl ("http://busit.co.nz/mobile/Journey-planner/");
//		}

		public void OnUpdatesBtnClick (object sender, EventArgs e)
		{
//			var activity2 = new Intent (this, typeof(WebActivity));
//			activity2.PutExtra("url","http://busit.co.nz/mobile/Service-updates/");
//			StartActivity (activity2);
			StartActivity (typeof(UpdateActivity));

		}

		public void OnPlannerBtnClick (object sender, EventArgs e)
		{
			var activity2 = new Intent (this, typeof(WebActivity));
			activity2.PutExtra("url","http://busit.co.nz/mobile/Journey-planner/");
			StartActivity (activity2);
		}
	}
}


