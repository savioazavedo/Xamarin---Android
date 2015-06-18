
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

namespace ToDoList
{
	[Activity (Label = "EditMain")]			
	public class EditMain : Activity
	{
		int ListId;
		string Title;
		string Details;

		TextView txtTitle;
		TextView txtDetails;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			SetContentView (Resource.Layout.EditMain);

			txtTitle = FindViewById<TextView> (Resource.Id.lblTitle);
			txtDetails = FindViewById<TextView> (Resource.Id.lblDescription);

			ListId = Intent.GetIntExtra ("ListID", 0);
			Details = Intent.GetStringExtra ("Details");
			Title = Intent.GetStringExtra ("Title");

			txtTitle.Text = Title;
			txtDetails.Text = Details;

			//objDb = new DatabaseManager ();
		}
	}
}

