
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
	[Activity (Label = "FreeList", Icon = "@drawable/ic_launcher",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
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
			objRest = new RestHandler(@"http://api.eventfinder.co.nz/v2/events.xml?free=1");
			var Response = await objRest.ExecuteRequestAsync ();
			tmpFreeList = Response.Event;

//			foreach(List <int> Event  in tmpFreeList1) {
//
//				PriceMax =	tmpFreeList1[Event].Ticket_types.Ticket_type [0].Price;
//				if (PriceMax == "0.00") {
//					tmpFreeList = tmpFreeList1;
					lstFreeEvents.Adapter = new DataAdapter (this, tmpFreeList);

		}
		public void OnlstFreeEventsClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var FreeItem = tmpFreeList [e.Position];

			var FreeDetail = new Intent (this, typeof(Detail));
			FreeDetail.PutExtra ("Title", FreeItem.Name);
			FreeDetail.PutExtra ("Address", FreeItem.Address);
			FreeDetail.PutExtra ("DateTime", FreeItem.Datetime_start);
			FreeDetail.PutExtra ("Image", FreeItem.Images.Image[0].Transforms.Transform[3].Url);
			FreeDetail.PutExtra ("Restriction", FreeItem.Restrictions);
			if (FreeItem.Ticket_types.Ticket_type.Count > 0) {
				FreeDetail.PutExtra ("TicketInformation", FreeItem.Ticket_types.Ticket_type [0].Price);
			} else {
				FreeDetail.PutExtra ("TicketInformation", "none");
			}
			FreeDetail.PutExtra ("Description", FreeItem.Description);
			FreeDetail.PutExtra ("Website", FreeItem.Url);

			FreeDetail.PutExtra ("LatitudeMap", FreeItem.Point.Lat);

			FreeDetail.PutExtra ("LongitudeinMap", FreeItem.Point.Lng);

			StartActivity (FreeDetail);


		}
	}
}

