
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
	[Activity (Label = "EditItem")]			
	public class EditItem : SherlockActivity
	{

		Button btnEdit;
		Button btnCancel;
		ActionMode mMode;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			SetContentView (Resource.Layout.ActionMode);

			btnEdit = FindViewById<Button> (Resource.Id.btnEdit);
			btnCancel = FindViewById<Button> (Resource.Id.btnCancel);

			btnEdit.Click += OnEditClick;
			btnCancel.Click += OnCancelClick;
		}

		void OnCancelClick (object sender, EventArgs e)
		{
			if (mMode != null) {
				mMode.Finish ();
			}
		}

		void OnEditClick (object sender, EventArgs e)
		{
			mMode = StartActionMode (new AnActionModeOfEpicProportions (this));
		}

		public void Message()
		{
			Toast.MakeText (this, "Action Mode Demo", ToastLength.Long).Show();
		}


		class AnActionModeOfEpicProportions : Java.Lang.Object, ActionMode.ICallback 
		{
			EditItem owner;

			public AnActionModeOfEpicProportions (EditItem owner)
			{
				this.owner = owner;
			}

			public bool OnCreateActionMode(ActionMode mode, IMenu menu) 
			{
				menu.Add ("Cancel")
					.SetIcon (Android.Resource.Drawable.IcMenuDelete)
					.SetShowAsAction (MenuItem.ShowAsActionAlways);

				return true;
			}

			public bool OnPrepareActionMode(ActionMode mode, IMenu menu) 
			{
				return false;
			}

			public bool OnActionItemClicked(ActionMode mode, IMenuItem item) 
			{

				var itemTitle = item.TitleFormatted.ToString();

				switch (itemTitle)
				{
				case "Cancel":
					mode.Finish ();
					break;
				}

				return true;
			}

			public void OnDestroyActionMode(ActionMode mode) 
			{
				owner.Message();
			}
		}
			
	}
}

