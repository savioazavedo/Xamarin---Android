
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

namespace ToDoList
{
	[Activity (Label = "ToDoList")]			
	public class AddItem : SherlockActivity
	{
		Button btnAdd;
		EditText txtItemDescription;
		EditText txtItemTitle;

		ActionMode mMode;

		DatabaseManager objdb = new DatabaseManager();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AddItem);

			txtItemTitle = FindViewById<EditText> (Resource.Id.txtItemTitle);
			txtItemDescription = FindViewById<EditText> (Resource.Id.txtItemDescription);

			txtItemTitle.Click += OnToDoItemEdit;
			txtItemDescription.Click += OnToDoItemEdit;

		}

		void OnToDoItemEdit (object sender, EventArgs e)
		{
			mMode = StartActionMode (new AnActionModeOfEpicProportions (this));
		}
			
		private void OnAddClick()
		{
			if (txtItemTitle.Text != "" && txtItemDescription.Text != "")
			{
				objdb.AddItem (txtItemTitle.Text, txtItemDescription.Text);

				Toast.MakeText (this, "Note Added", ToastLength.Long).Show();
				this.Finish ();
				StartActivity (typeof(MainActivity));
			}
		}


		class AnActionModeOfEpicProportions : Java.Lang.Object, ActionMode.ICallback {
			AddItem owner;
			public AnActionModeOfEpicProportions (AddItem owner)
			{
				this.owner = owner;
			}
			public bool OnCreateActionMode(ActionMode mode, IMenu menu) {
				menu.Add ("Cancel")
					.SetIcon (Android.Resource.Drawable.IcMenuDelete)
					.SetShowAsAction (MenuItem.ShowAsActionWithText);

				return true;
			}

			public bool OnPrepareActionMode(ActionMode mode, IMenu menu) {
				return false;
			}

			public bool OnActionItemClicked(ActionMode mode, IMenuItem item) {
			
				var itemTitle = item.TitleFormatted.ToString();
				switch (itemTitle)
				{
				case "Cancel":
					//mode.Finish ();
					owner.Finish ();
					owner.StartActivity (typeof(MainActivity));
					break;
				}

				return true;
			}

			public void OnDestroyActionMode(ActionMode mode) {
				//mode.Finish ();
				owner.OnAddClick ();
			}
		}
			
	}
}

