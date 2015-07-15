using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;

namespace MaterialDesign
{
	[Activity (Label = "MaterialDesign", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
		int count = 1;

		DrawerLayout drawerLayout;
		NavigationView navigationView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);
			var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

			// Set our view from the "main" layout resource

			SetSupportActionBar (toolbar);
			//Enable support action bar to display hamburger
			SupportActionBar.SetHomeAsUpIndicator (Resource.Drawable.abc_ic_menu_copy_mtrl_am_alpha);
			SupportActionBar.SetDisplayHomeAsUpEnabled (true);
			drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			navigationView = FindViewById<NavigationView> (Resource.Id.nav_view);
			navigationView.NavigationItemSelected += (sender, e) => {
				e.MenuItem.SetChecked (true);
				//react to click here and swap fragments or navigate
				drawerLayout.CloseDrawers ();
			};
			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {

					
			};
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				drawerLayout.OpenDrawer (Android.Support.V4.View.GravityCompat.Start);
				return true;
			}
			return base.OnOptionsItemSelected (item);
		}

	}
}


