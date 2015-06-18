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

namespace BusLookATour
{
	[Activity (Label = "UpdateActivity")]			
	public class UpdateActivity : Activity
	{
		ListView lstItems;
		List<Busit> UpdatesList = new List<Busit> ();

		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ListActivity);

			lstItems = FindViewById<ListView> (Resource.Id.lstItems);

			lstItems.ItemClick += OnlstItemsClick;

			LoadToDoItems ();
		}

		public async void LoadToDoItems()
		{
			UpdatesList = await objParse.GetAll ();
			lstItems.Adapter = new DataAdapter (this, UpdatesList);
		}

		void OnlstItemsClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var ServiceUpdate = UpdatesList [e.Position];
			var edititem = new Intent (this, typeof(ServiceUpdatesActivity));


			edititem.PutExtra ("Title", ServiceUpdate.Title);
			edititem.PutExtra ("Description", ServiceUpdate.Description);
			edititem.PutExtra ("Details", ServiceUpdate.Details);
			edititem.PutExtra ("DateFrom", ServiceUpdate.DateFrom);
			edititem.PutExtra ("DateTo", ServiceUpdate.DateTo);
			edititem.PutExtra ("ListID", ServiceUpdate.ObjectId);

			StartActivity (edititem);
			//StartActivity (typeof(ServiceUpdatesActivity));
		}
	}
}