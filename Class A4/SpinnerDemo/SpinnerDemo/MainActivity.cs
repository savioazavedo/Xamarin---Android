using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SpinnerDemo
{
	[Activity (Label = "SpinnerDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Spinner spCity;
		ImageView imgCity;
	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it

			spCity = FindViewById<Spinner> (Resource.Id.spCity);
			imgCity = FindViewById<ImageView> (Resource.Id.imgCity);

			spCity.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
         

            var adapter = ArrayAdapter.CreateFromResource (this, Resource.Array.City_Names, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spCity.Adapter = adapter;
		}


        public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;

			if (String.Format("{0}",spinner.GetItemAtPosition (e.Position)) == "Hamilton"){
				imgCity.SetImageResource (Resource.Drawable.hamilton);
			} else if (String.Format("{0}",spinner.GetItemAtPosition (e.Position)) == "Auckland") {
				imgCity.SetImageResource (Resource.Drawable.auckland);
			} else if (String.Format("{0}",spinner.GetItemAtPosition (e.Position)) == "Christchurch") {
				imgCity.SetImageResource (Resource.Drawable.christchurch);
			} else if (String.Format("{0}",spinner.GetItemAtPosition (e.Position)) == "Wellington") {
				imgCity.SetImageResource (Resource.Drawable.wellington);
			}

		}

	

	}
}


