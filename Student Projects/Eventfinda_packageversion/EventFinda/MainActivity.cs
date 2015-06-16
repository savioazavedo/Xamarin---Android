using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Linq;
using System.Threading;
using Android.Net;

namespace EventFinda
{
	[Activity (Label = "Eventure",   Icon = "@drawable/OnlyLogo",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity, ILocationListener
	{ 
		string lat;
		string lng;
		LocationManager locMgr;
		Button btnPopular;
		Button btnSearch;
		Button btnNearby;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Menu);
			btnPopular = FindViewById <Button> (Resource.Id.btnPopular);
			btnSearch = FindViewById <Button> (Resource.Id.btnSearch);
			btnNearby = FindViewById <Button> (Resource.Id.btnNearby);
			btnPopular.Click += OnbtnPopularClick;
			btnNearby.Click += OnbtnNearbyClick;
			btnSearch.Click += OnbtnSearchClick;
			GetLocation ();
		}


		protected override void OnResume ()
		{
			base.OnResume ();

		}
		void GetLocation ()
		{
			locMgr = GetSystemService (Context.LocationService) as LocationManager;
			Criteria LocationCriteria = new Criteria();
			LocationCriteria.PowerRequirement = Power.Medium;
			string locationProvider = locMgr.GetBestProvider (LocationCriteria, true);
			if (locationProvider != null) {
				locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);
			} else {
				Toast.MakeText(this, "No Location provider available",  ToastLength.Short).Show ();
			}

		}
		public void OnLocationChanged (Android.Locations.Location location)
		{
			 lat = location.Latitude.ToString ();
			 lng = location.Longitude.ToString ();

		}
		public void OnProviderEnabled (string provider)
		{
			//Toast.MakeText (this, "provider enabled", ToastLength.Short).Show ();
		}
		public void OnProviderDisabled (string provider)
		{
			//Toast.MakeText (this, "provider disabled", ToastLength.Short).Show ();
		}
		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			//Toast.MakeText (this, "provider status" + status.ToString (), ToastLength.Short).Show ();
		}


		private Boolean CheckConnectivity()
		{
			var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;

			if ((activeConnection != null) && activeConnection.IsConnected) {
				return true;
			} else {
				return false;
			}
		}


		void OnbtnPopularClick (object sender, EventArgs e)
		{
			if (CheckConnectivity ()) {
				StartActivity (typeof(PopularList));
			} else {
				Toast.MakeText (this, "Not connected to WIFI or Network.Please check your Internet connection and try again",ToastLength.Long).Show ();
			}
		}

		void OnbtnNearbyClick (object sender, EventArgs e)
		{   
		
			if (CheckConnectivity ()) {
				var NearbyList = new Intent (this, typeof(NearbyList));
				NearbyList.PutExtra ("Latitude", lat);
				NearbyList.PutExtra ("Longitude", lng);
				StartActivity (NearbyList);
			} else {
				Toast.MakeText (this, "Not connected to WIFI or Network.Please check your Internet connection and try again",ToastLength.Long).Show ();
			}

		}
		void OnbtnSearchClick (object sender, EventArgs e)
		{
					
			if (CheckConnectivity ()) {
				StartActivity (typeof(SearchEvent));	
			} else {
				Toast.MakeText (this, "Not connected to WIFI or Network.Please check your Internet connection and try again",ToastLength.Long).Show ();
			}

		}
	
	}
}


