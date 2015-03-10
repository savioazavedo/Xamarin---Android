using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SimpleList
{
	[Activity (Label = "SimpleList", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{
		int count = 1;
		string[] items;

		Button btnCall;
		Button btnOpenMap;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items);


		}


		public void InitializeControls()
		{
			//btnCall = FindViewById<Button>
		}

	}
}


