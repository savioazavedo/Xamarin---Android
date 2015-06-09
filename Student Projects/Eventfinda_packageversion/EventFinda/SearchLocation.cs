
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

namespace EventFinda
{
	[Activity (Label = "SearchLocation", Icon = "@drawable/ic_launcher",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class SearchLocation : Activity
	{

		RestHandler objRest;
		ListView lstEventsSearchbyLocation;
		List <Event> tmpEventsSearchByLocation;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			lstEventsSearchbyLocation = FindViewById<ListView> (Resource.Id.listView1);
			lstEventsSearchbyLocation.ItemClick += OnlstEventsSearchbyLocationClick;	
			LoadSearchEventsbyLocation ();
			// Create your application here
		}
		public async void LoadSearchEventsbyLocation ()
		{
		var searchlocations= Intent.GetStringExtra("SearchLocations");

		objRest = new RestHandler (@"http://api.eventfinder.co.nz/v2/events.xml?autocomplete="+ searchlocations +"&fields=Location:(name)");
		var Response = await objRest.ExecuteRequestAsync ();
		lstEventsSearchbyLocation.Adapter = new DataAdapter (this, Response.Event);
		tmpEventsSearchByLocation = Response.Event;
	}
		void OnlstEventsSearchbyLocationClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var EventSearchbyLocationItem = tmpEventsSearchByLocation [e.Position];

			var EventSearchbyLocationDetail = new Intent (this, typeof(Detail));

			Helper objHelper = new Helper ();

			EventSearchbyLocationDetail.PutExtra ("Title", objHelper.removecdata(EventSearchbyLocationItem.Name));
			EventSearchbyLocationDetail.PutExtra ("Address", objHelper.removecdata(EventSearchbyLocationItem.Address));
			EventSearchbyLocationDetail.PutExtra ("DateTime", EventSearchbyLocationItem.Datetime_start);
			EventSearchbyLocationDetail.PutExtra ("Image", EventSearchbyLocationItem.Images.Image[0].Transforms.Transform[3].Url);
			EventSearchbyLocationDetail.PutExtra ("Restriction", EventSearchbyLocationItem.Restrictions);
			if (EventSearchbyLocationItem.Ticket_types.Ticket_type.Count > 0) {
				EventSearchbyLocationDetail.PutExtra ("TicketInformation", EventSearchbyLocationItem.Ticket_types.Ticket_type [0].Price);
			} else {
				EventSearchbyLocationDetail.PutExtra ("TicketInformation", "none");
			}
			EventSearchbyLocationDetail.PutExtra ("Description",objHelper.removecdata(EventSearchbyLocationItem.Description));
			EventSearchbyLocationDetail.PutExtra ("Website", EventSearchbyLocationItem.Url);
			Toast.MakeText (this, "latitude" + EventSearchbyLocationItem.Point.Lat, ToastLength.Short).Show ();
			EventSearchbyLocationDetail.PutExtra ("LatitudeMap", EventSearchbyLocationItem.Point.Lat);

			EventSearchbyLocationDetail.PutExtra ("LongitudeinMap",EventSearchbyLocationItem.Point.Lng);

			StartActivity (EventSearchbyLocationDetail);
		}

	}
}

