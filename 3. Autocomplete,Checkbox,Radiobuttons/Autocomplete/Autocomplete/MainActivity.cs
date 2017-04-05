﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Java.IO;

namespace Autocomplete
{
	[Activity (Label = "Autocomplete", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : Activity
	{
		AutoCompleteTextView acweekdays;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            //var weekday_array = Resources.GetStringArray(Resource.Array.weekdays_array);

			var weekday_array = new string[] { "Monday" , "Tuesday" , "Wednesday" , "Thursday" , "Friday" , "Saturday" , "Sunday","Wednesday-2","Sunday-2"};
			acweekdays = FindViewById<AutoCompleteTextView> (Resource.Id.actxtWeekdays);
			acweekdays.Adapter = new ArrayAdapter<string> (this,Android.Resource.Layout.TestListItem,weekday_array);

            InputStream input = Assets.Open("my_asset.txt");


        }
	}

}


