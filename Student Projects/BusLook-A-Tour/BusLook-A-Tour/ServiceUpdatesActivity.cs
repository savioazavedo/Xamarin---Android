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
	[Activity (Label = "ServiceUpdatesActivity")]			
	public class ServiceUpdatesActivity : Activity
	{

		int ListId;
		string Title;
		string Details;
		string Description;
		string DateFrom;
		string DateTo;

		TextView txtTitle;
		TextView txtDetails;
		//TextView txtDescription;
		//TextView txtDateFrom;
		//TextView txtDateTo;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ServiceUpdates);

			txtTitle = FindViewById<TextView> (Resource.Id.txtTitle);
			txtDetails = FindViewById<TextView> (Resource.Id.txtDetails);
			//txtDescription = FindViewById<TextView> (Resource.Id.txtDescription);
			//txtDateFrom = FindViewById<TextView> (Resource.Id.txtDateFrom);
			//txtDateTo = FindViewById<TextView> (Resource.Id.txtDateTo);

			ListId = Intent.GetIntExtra ("ListID", 0);
			Title = Intent.GetStringExtra ("Title");
			Description = Intent.GetStringExtra ("Description");
			DateFrom = Intent.GetStringExtra ("DateFrom");
			DateTo = Intent.GetStringExtra ("DateTo");
			Details = Intent.GetStringExtra ("Details");


			txtTitle.Text = Title;
			txtDetails.Text = Details;
			//txtDescription.Text = Description;
			//txtDateTo.Text = DateTo;
			//txtDateFrom.Text = DateFrom;

		}
	}
}

