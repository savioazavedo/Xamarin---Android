
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
using Xamarin.ActionbarSherlockBinding.Views;
using ActionProvider = Xamarin.ActionbarSherlockBinding.Views.ActionProvider;
using ActionMode = Xamarin.ActionbarSherlockBinding.Views.ActionMode;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using IMenuItem = Xamarin.ActionbarSherlockBinding.Views.IMenuItem;
using Android.Provider;

namespace ToDoList
{
	[Activity (Label = "EditItem")]			
	public class EditItem : SherlockActivity
	{

		int ListId;
		string Title;
		string Details;

		TextView txtTitle;
		TextView txtDetails;
		DatabaseManager objDb;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.EditItem);

			txtTitle = FindViewById<TextView> (Resource.Id.txtEditTitle);
			txtDetails = FindViewById<TextView> (Resource.Id.txtEditDescription);

			ListId = Intent.GetIntExtra("ListID",0);
			Details = Intent.GetStringExtra("Details");
			Title = Intent.GetStringExtra("Title");

			txtTitle.Text = Title;
			txtDetails.Text = Details;

			objDb = new DatabaseManager();

		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{

			menu.Add ("Save")
				.SetIcon (Android.Resource.Drawable.IcMenuSave)
				.SetShowAsAction (MenuItem.ShowAsActionAlways);

			menu.Add ("Delete")
				.SetIcon (Android.Resource.Drawable.IcMenuDelete)
				.SetShowAsAction (MenuItem.ShowAsActionAlways);
				
			return true;
		}
			
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString();

			switch (itemTitle)
			{
			case "Save":
				OnItemSave ();
				break;

			case "Delete":
				OnItemDelete ();
				break;
			}
			return base.OnOptionsItemSelected(item);
		}
			
		public void OnItemSave()
		{
			try 
			{
				objDb.EditItem(txtTitle.Text,txtDetails.Text,ListId);
				Toast.MakeText(this,"Changes Saved",ToastLength.Long).Show();
				this.Finish();
				StartActivity(typeof(MainActivity));
			} catch (Exception ex) {
				Console.WriteLine ("Error Occurred:" + ex.Message);
			}
		}

		public void OnItemDelete()
		{
			try 
			{
				objDb.DeleteItem(ListId);
				Toast.MakeText(this,"Note Deleted",ToastLength.Long).Show();
				this.Finish();
				StartActivity(typeof(MainActivity));
			} catch (Exception ex) {
				Console.WriteLine ("Error Occurred:" + ex.Message);
			}
		}
			
	}
}

