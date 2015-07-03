
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

using Android.Support.V4.Widget;
using Xamarin.ActionbarSherlockBinding;
using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;
using Xamarin.ActionbarSherlockBinding.Widget;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using SearchView = Xamarin.ActionbarSherlockBinding.Widget.SearchView;
using CursorAdapter = Android.Support.V4.Widget.CursorAdapter;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using System.IO;
using System.Collections.Generic;

namespace SingHallelujah
{
	[Activity (Label = "Sing Hallelujah", Icon = "@drawable/icon")]			
	public class FullSong : SherlockActivity
	{

		int SongId;
		Boolean bFavorite;
		CheckBox cbFavorite;
		string SongTitle;
		string SongLyrics;
		TextView txtSongTitle;
		TextView txtSongLyrics;
		DatabaseManager objDb = new DatabaseManager();


		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			// Inflate your menu.
			SupportMenuInflater.Inflate (Resource.Menu.share_action_provider, menu);

			ActionBar.SetHomeButtonEnabled(true);
			ActionBar.SetDisplayHomeAsUpEnabled (true);

			var actionItem = menu.FindItem (Resource.Id.menu_item_share_action_provider_action_bar);
			var actionProvider = (Xamarin.ActionbarSherlockBinding.Widget.ShareActionProvider)actionItem.ActionProvider;
			//actionProvider.SetShareHistoryFileName (Xamarin.ActionbarSherlockBinding.Widget.ShareActionProvider.DefaultShareHistoryFileName);

			actionProvider.SetShareIntent (CreateShareIntent ());
		
			return true;
		}

		private Intent CreateShareIntent ()
		{
			Intent shareIntent = new Intent (Intent.ActionSend);

			shareIntent.SetType("text/plain");
			shareIntent.PutExtra(Android.Content.Intent.ExtraSubject,txtSongTitle.Text);
			shareIntent.PutExtra(Android.Content.Intent.ExtraText,txtSongLyrics.Text);

			return shareIntent;
		}

		public override bool OnOptionsItemSelected(Xamarin.ActionbarSherlockBinding.Views.IMenuItem item)
		{
			switch (item.ItemId)
			{
			case Android.Resource.Id.Home:
				Finish();
				return true;

			default:
				return base.OnOptionsItemSelected(item);
			}
		}

		protected override void OnCreate (Bundle bundle)
		{

			SetTheme (Resource.Style.Theme_Sherlock_Light_DarkActionBar);
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.FullSong);

			txtSongTitle = FindViewById<TextView> (Resource.Id.txtSongTitle);
			txtSongLyrics = FindViewById<TextView> (Resource.Id.txtSongLyrics);
			cbFavorite = FindViewById<CheckBox> (Resource.Id.cbxStart);

			cbFavorite.CheckedChange += OnFavoriteClick;

			SongId = Intent.GetIntExtra("SongId",0);
			SongTitle = Intent.GetStringExtra("SongTitle");
			SongLyrics = Intent.GetStringExtra("SongLyrics");
			bFavorite = Intent.GetBooleanExtra("Favorite",false);

			txtSongTitle.Text = SongTitle;
			txtSongLyrics.Text = Helper.Instance.RemoveQuote(SongLyrics);
		
			cbFavorite.Checked = objDb.CheckFavorite (SongId);

		}

		void OnFavoriteClick (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			if (cbFavorite.Checked == true) {
				objDb.AddToFavorites(SongId,true);
				Toast.MakeText (this,"Added to favorites",ToastLength.Short).Show ();
			} else {
				objDb.AddToFavorites(SongId,false);
				Toast.MakeText (this,"Removed from favorites",ToastLength.Short).Show ();
			}
		}

	}
}

