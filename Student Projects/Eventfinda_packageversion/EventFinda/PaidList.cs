
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
	[Activity (Label = "Paid Events",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait,Icon = "@drawable/OnlyLogo")]			
	public class PaidList : Activity
	{
		RestHandler objRest;
		ListView lstPaidEvents;
		List <Event> tmpPaidList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ListView);
			lstPaidEvents = FindViewById<ListView> (Resource.Id.listView1);
			lstPaidEvents.ItemClick += OnlstPaidEventsClick;	
			LoadPaidEvents ();
		}

		public async void LoadPaidEvents ()
		{
			AndHUD.Shared.Show(this, "Finding paid events", 60);
			objRest = new RestHandler(@"http://api.eventfinder.co.nz/v2/events.xml?free=0");
			var Response = await objRest.ExecuteRequestAsync ();
			tmpPaidList = Response.Event;

			//			foreach(List <int> Event  in tmpFreeList1) {
			//
			//				PriceMax =	tmpFreeList1[Event].Ticket_types.Ticket_type [0].Price;
			//				if (PriceMax == "0.00") {
			//					tmpFreeList = tmpFreeList1;
			lstPaidEvents.Adapter = new DataAdapter (this, tmpPaidList);
			AndHUD.Shared.Dismiss ();

		}
		public void OnlstPaidEventsClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var PaidItem = tmpPaidList [e.Position];

			Helper objHelper = new Helper ();

			var PaidDetail = new Intent (this, typeof(Detail));
			PaidDetail.PutExtra ("Title",  objHelper.removecdata(PaidItem.Name));
			PaidDetail.PutExtra ("Address",  objHelper.removecdata(PaidItem.Address));
			PaidDetail.PutExtra ("DateTime", PaidItem.Datetime_start);
			PaidDetail.PutExtra ("Image", PaidItem.Images.Image[0].Transforms.Transform[3].Url);
			PaidDetail.PutExtra ("Restriction", PaidItem.Restrictions);
			if (PaidItem.Ticket_types.Ticket_type.Count > 0) {
				PaidDetail.PutExtra ("TicketInformation", PaidItem.Ticket_types.Ticket_type [0].Price);
			} else {
				PaidDetail.PutExtra ("TicketInformation", "none");
			}
			PaidDetail.PutExtra ("Description",objHelper.removecdata(PaidItem.Description));
			PaidDetail.PutExtra ("Website", PaidItem.Url);

			PaidDetail.PutExtra ("LatitudeMap", PaidItem.Point.Lat);

			PaidDetail.PutExtra ("LongitudeinMap", PaidItem.Point.Lng);

			StartActivity (PaidDetail);


		}
	}
}

