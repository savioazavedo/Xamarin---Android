
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
using AndroidHUD;

namespace EventFinda
{
	[Activity (Label = "Nearby Events", Icon = "@drawable/OnlyLogo",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class NearbyList : Activity
	{
		RestHandler objRest;
		ListView lstNearbyEvents;
		List <Event> tmpNearbyList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			lstNearbyEvents = FindViewById<ListView> (Resource.Id.listView1);
			lstNearbyEvents.ItemClick += OnlstNearbyEventsClick;
			LoadNearbyEvents ();

			// Create your application here
		}
		public async void LoadNearbyEvents()
		{
			AndHUD.Shared.Show(this, "Finding nearby events", 60);
			var lat= Convert.ToDouble (Intent.GetStringExtra("Latitude"));
			var lng = Convert.ToDouble(Intent.GetStringExtra("Longitude"));
			objRest = new RestHandler (@"http://api.eventfinder.co.nz/v2/events.xml?point="+ lat + "," + lng + "&radius=20");
			var Response = await objRest.ExecuteRequestAsync ();
			lstNearbyEvents.Adapter = new DataAdapter (this, Response.Event);
			tmpNearbyList = Response.Event;
			AndHUD.Shared.Dismiss ();

		}
		void OnlstNearbyEventsClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var NearbyItem = tmpNearbyList [e.Position];

			var NearbyDetail = new Intent (this, typeof(Detail));

			Helper objHelper = new Helper ();

			NearbyDetail.PutExtra ("Title", objHelper.removecdata(NearbyItem.Name));
			NearbyDetail.PutExtra ("Address", objHelper.removecdata(NearbyItem.Address));
			NearbyDetail.PutExtra ("DateTime", NearbyItem.Datetime_start);
			NearbyDetail.PutExtra ("Image", NearbyItem.Images.Image[0].Transforms.Transform[3].Url);
			NearbyDetail.PutExtra ("Restriction", NearbyItem.Restrictions);
			if (NearbyItem.Ticket_types.Ticket_type.Count > 0) {
				NearbyDetail.PutExtra ("TicketInformation", NearbyItem.Ticket_types.Ticket_type [0].Price);
			} else {
				NearbyDetail.PutExtra ("TicketInformation", "none");
			}
			NearbyDetail.PutExtra ("Description", objHelper.removecdata(NearbyItem.Description));
			NearbyDetail.PutExtra ("Website", NearbyItem.Url);
			//Toast.MakeText (this, "latitude" + NearbyItem.Point.Lat, ToastLength.Short).Show ();
			NearbyDetail.PutExtra ("LatitudeMap", NearbyItem.Point.Lat);

			NearbyDetail.PutExtra ("LongitudeinMap", NearbyItem.Point.Lng);

			StartActivity (NearbyDetail);
		}
	}
}

