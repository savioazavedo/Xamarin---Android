using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SimpleList2
{
	[Activity (Label = "SimpleList2", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{
		string[] items;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			items = new string[] { "Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers" };
			ListView.ChoiceMode = ChoiceMode.Single;
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemSingleChoice, items);
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			var t = items[position];
			Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
		}
	}
}
	