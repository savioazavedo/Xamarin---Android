
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
	[Activity (Label = "Free Events", Icon = "@drawable/OnlyLogo",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class FreeList : Activity
	{
		RestHandler objRest;
		ListView lstFreeEvents;
		List <Event> tmpFreeList;
	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			lstFreeEvents = FindViewById<ListView> (Resource.Id.listView1);
			lstFreeEvents.ItemClick += OnlstFreeEventsClick;	
			LoadFreeEvents ();
			// Create your application here
		}
		public async void LoadFreeEvents ()
		{
			AndHUD.Shared.Show(this, "Finding free events", 60);
			objRest = new RestHandler(@"http://api.eventfinder.co.nz/v2/events.xml?free=1");
			var Response = await objRest.ExecuteRequestAsync ();
			tmpFreeList = Response.Event;

//			foreach(List <int> Event  in tmpFreeList1) {
//
//				PriceMax =	tmpFreeList1[Event].Ticket_types.Ticket_type [0].Price;
//				if (PriceMax == "0.00") {
//					tmpFreeList = tmpFreeList1;
			lstFreeEvents.Adapter = new DataAdapter (this, tmpFreeList);
			AndHUD.Shared.Dismiss ();

		}
		public void OnlstFreeEventsClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var FreeItem = tmpFreeList [e.Position];

			Helper objHelper = new Helper ();

			var FreeDetail = new Intent (this, typeof(Detail));
			FreeDetail.PutExtra ("Title", objHelper.removecdata(FreeItem.Name));
			FreeDetail.PutExtra ("Address", objHelper.removecdata(FreeItem.Address));
			FreeDetail.PutExtra ("DateTime", FreeItem.Datetime_start);
			FreeDetail.PutExtra ("Image", FreeItem.Images.Image[0].Transforms.Transform[3].Url);
			FreeDetail.PutExtra ("Restriction", FreeItem.Restrictions);
			if (FreeItem.Ticket_types.Ticket_type.Count > 0) {
				FreeDetail.PutExtra ("TicketInformation", FreeItem.Ticket_types.Ticket_type [0].Price);
			} else {
				FreeDetail.PutExtra ("TicketInformation", "none");
			}
			FreeDetail.PutExtra ("Description", objHelper.removecdata(FreeItem.Description));
			FreeDetail.PutExtra ("Website", FreeItem.Url);

			FreeDetail.PutExtra ("LatitudeMap", FreeItem.Point.Lat);

			FreeDetail.PutExtra ("LongitudeinMap", FreeItem.Point.Lng);

			StartActivity (FreeDetail);


		}
	}
}

