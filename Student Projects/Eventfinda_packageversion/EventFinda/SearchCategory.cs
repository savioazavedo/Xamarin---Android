
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
	[Activity (Label = "Search Results", Icon = "@drawable/OnlyLogo",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class SearchCategory : Activity
	{
		RestHandler objRest;
		ListView lstEventsSearchbyCategory;
		List <Event> tmpEventsSearchByCategory;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			lstEventsSearchbyCategory = FindViewById<ListView> (Resource.Id.listView1);
			lstEventsSearchbyCategory.ItemClick += OnlstEventsSearchbyCategoryClick;	
			LoadSearchEventsbyCategory ();
			// Create your application here
		}

		public async void LoadSearchEventsbyCategory ()
		{
			var searchcategory= Intent.GetStringExtra("SearchCategory");

			AndHUD.Shared.Show(this, "Searching events", 60);
			objRest = new RestHandler (@"http://api.eventfinder.co.nz/v2/events.xml?autocomplete="+ searchcategory +"&fields=Category:(name)");
			var Response = await objRest.ExecuteRequestAsync ();
			lstEventsSearchbyCategory.Adapter = new DataAdapter (this, Response.Event);
			tmpEventsSearchByCategory = Response.Event;
			AndHUD.Shared.Dismiss();
		}
		void OnlstEventsSearchbyCategoryClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var EventSearchbyCategoryItem = tmpEventsSearchByCategory [e.Position];

			var EventSearchbyCategoryDetail = new Intent (this, typeof(Detail));

			Helper objHelper = new Helper ();

			EventSearchbyCategoryDetail.PutExtra ("Title", objHelper.removecdata(EventSearchbyCategoryItem.Name));
			EventSearchbyCategoryDetail.PutExtra ("Address", objHelper.removecdata(EventSearchbyCategoryItem.Address));
			EventSearchbyCategoryDetail.PutExtra ("DateTime", EventSearchbyCategoryItem.Datetime_start);
			EventSearchbyCategoryDetail.PutExtra ("Image", EventSearchbyCategoryItem.Images.Image[0].Transforms.Transform[3].Url);
			EventSearchbyCategoryDetail.PutExtra ("Restriction", EventSearchbyCategoryItem.Restrictions);
			if (EventSearchbyCategoryItem.Ticket_types.Ticket_type.Count > 0) {
				EventSearchbyCategoryDetail.PutExtra ("TicketInformation", EventSearchbyCategoryItem.Ticket_types.Ticket_type [0].Price);
			} else {
				EventSearchbyCategoryDetail.PutExtra ("TicketInformation", "none");
			}
			EventSearchbyCategoryDetail.PutExtra ("Description",objHelper.removecdata(EventSearchbyCategoryItem.Description));
			EventSearchbyCategoryDetail.PutExtra ("Website", EventSearchbyCategoryItem.Url);
			//Toast.MakeText (this, "latitude" + EventSearchbyCategoryItem.Point.Lat, ToastLength.Short).Show ();
			EventSearchbyCategoryDetail.PutExtra ("LatitudeMap", EventSearchbyCategoryItem.Point.Lat);

			EventSearchbyCategoryDetail.PutExtra ("LongitudeinMap",EventSearchbyCategoryItem.Point.Lng);

			StartActivity (EventSearchbyCategoryDetail);
		}

	}
}

