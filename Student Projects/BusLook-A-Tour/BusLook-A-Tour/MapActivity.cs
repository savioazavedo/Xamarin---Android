
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
//using Android.Locations;
using Android.Locations;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace BusLookATour
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity, ILocationListener
	{
		LocationManager locMgr;
		GoogleMap map;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.MapView);


			MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById (Resource.Id.map);
			map = mapFrag.Map;

			if (map != null) {

				MarkerOptions opt1 = new MarkerOptions ();

				double lat = 37.7801062;
				double lng = 175.3002751;


				LatLng location = new LatLng (lat, lng);
				opt1.SetPosition (location);
				//opt1.SetTitle (a
				map.AddMarker (opt1);

				CameraPosition.Builder builder = CameraPosition.InvokeBuilder ();
				builder.Target (location);
				builder.Zoom (15);

				CameraPosition cameraPosition = builder.Build ();
				CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition (cameraPosition);

				map.MoveCamera (cameraUpdate);
			}



//				Criteria locationCriteria = new Criteria ();
//
//				locationCriteria.Accuracy = Accuracy.Coarse;
//				locationCriteria.PowerRequirement = Power.Medium;
//				locMgr = (LocationManager)GetSystemService (LocationService);
//				string locationProvider = locMgr.GetBestProvider (locationCriteria, true);
//
//				if (locationProvider != null) {
//					locMgr.RequestLocationUpdates (locationProvider, 2000, 1, this);
//					Toast.MakeText (this, "Provider:" + locationProvider, ToastLength.Short).Show ();
//				} else {
//					Toast.MakeText (this, "No location providers available", ToastLength.Short).Show ();
//				}

		}

		protected override void OnResume ()
		{
			base.OnResume ();
			locMgr = GetSystemService (Context.LocationService) as LocationManager;
		}

		protected override void OnPause()
		{
			base.OnPause ();
			locMgr.RemoveUpdates (this);
		}

		public void OnProviderEnabled (string provider)
		{
			Toast.MakeText (this, "Provider Enabled", ToastLength.Short).Show ();
		}

		public void OnProviderDisabled (string provider)
		{
			Toast.MakeText (this, "Provider Disabled", ToastLength.Short).Show ();
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			Toast.MakeText (this, "Provider Status" + status.ToString(),ToastLength.Short).Show();
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			location.Latitude.ToString ();
			location.Longitude.ToString ();
		}


	}
}

