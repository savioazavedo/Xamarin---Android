using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.ActionbarSherlockBinding.App;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using Xamarin.ActionbarSherlockBinding.Views;
using Tab = Xamarin.ActionbarSherlockBinding.App.ActionBar.Tab;
using FragmentTransaction = Android.Support.V4.App.FragmentTransaction;
using ActionProvider = Xamarin.ActionbarSherlockBinding.Views.ActionProvider;
using ActionMode = Xamarin.ActionbarSherlockBinding.Views.ActionMode;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using Android.Provider;

namespace ActionBarSherlockDemo
{
	[Activity (Label = "ActionBarSherlockDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : SherlockActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				StartActivity(typeof(EditItem));
			};
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{

			menu.Add ("Save")
				.SetIcon (Android.Resource.Drawable.IcMenuSave)
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom);

			menu.Add ("Search")
				.SetIcon (Android.Resource.Drawable.IcMenuSearch)
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom | MenuItem.ShowAsActionWithText);

			menu.Add ("Refresh")
				.SetIcon (Android.Resource.Drawable.IcMenuRevert)
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom | MenuItem.ShowAsActionWithText);

			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString();

			switch (itemTitle)
			{

			case "Save":
				Toast.MakeText (this,"Save clicked",ToastLength.Long).Show ();
				break;

			case "Search":
				Toast.MakeText (this, "Search clicked", ToastLength.Long).Show ();
				break;

			case "Refresh":
				Toast.MakeText (this, "Refresh clicked", ToastLength.Long).Show ();
				break;
			}

			return base.OnOptionsItemSelected(item);
		}



	}
}


