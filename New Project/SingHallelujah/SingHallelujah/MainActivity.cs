using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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
using AndroidHUD;


namespace SingHallelujah
{
	[Activity (Label = "Sing Hallelujah", Icon = "@drawable/icon")]

	public class MainActivity : SherlockActivity,SearchView.IOnQueryTextListener,SearchView.IOnSuggestionListener
	{
		//int count = 1;

		const int ShakeDetectionTimeLapse = 450;
		const double ShakeThreshold = 500;
			
		ListView lstSongList;
		List<Song> myList;
		static string dbName = "SingHalleluiah.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);
		DatabaseManager objDb = new DatabaseManager();
		bool favorites= false;
		bool shuffle = false;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetTheme (Resource.Style.Theme_Sherlock_Light_DarkActionBar);

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			CopyDatabase ();

			lstSongList = FindViewById <ListView>(Resource.Id.lstSongs);
			LoadAll ();

			AndHUD.Shared.ShowToast(this, "Search for a song", MaskType.Clear, TimeSpan.FromSeconds(4));
			lstSongList.ItemClick += lstSongListClick; 

			lstSongList.FastScrollEnabled = true;
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			if (favorites == true) {
				LoadFavorites ();
			} else {
				LoadAll ();
			}
		}

		public void LoadFavorites()
		{
			myList = objDb.ViewAll().FindAll(p => p.Favorite == "True").ToList();
			lstSongList.Adapter = new DataAdapter(this,myList);
		}

		public void LoadAll()
		{
			myList = objDb.ViewAll();
			lstSongList.Adapter = new DataAdapter(this,myList);
		}

		void lstSongListClick (object sender, AdapterView.ItemClickEventArgs e)
		{

			var SongItem = myList[e.Position];
			var fullsong = new Intent (this, typeof(FullSong));

			fullsong.PutExtra ("SongId",SongItem.SongId);
			fullsong.PutExtra ("SongTitle", SongItem.SongName );
			fullsong.PutExtra ("SongLyrics", SongItem.Lyrics);
			fullsong.PutExtra ("Favorite",SongItem.Favorite);
		
			StartActivity (fullsong);
		}

		public void CopyDatabase()
		{
			// Check if your DB has already been extracted.

			try{

				if (!File.Exists(dbPath))
				{
					using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
					{
						using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
						{
							byte[] buffer = new byte[2048];
							int len = 0;
							while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
							{
								bw.Write (buffer, 0, len);
							}
						}
					}
				}

			} catch (Exception ex) {
				Toast.MakeText (this,"Error in copying the song database,Please contact the developer" + ex.Message,ToastLength.Long).Show ();
			}
		}
			
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			//Used to put dark icons on light action bar

			SearchView searchView = new SearchView (SupportActionBar.ThemedContext);
			searchView.QueryHint = "Search for a song...";
			searchView.SetOnQueryTextListener (this);
			searchView.SetOnSuggestionListener (this);

			//searchView.SuggestionsAdapter = mSuggestionsAdapter;

			menu.Add ("Search")
				.SetIcon (Resource.Drawable.abs__ic_search)
				.SetActionView (searchView)
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom | MenuItem.ShowAsActionCollapseActionView);

			menu.Add ("Favorites")
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom | MenuItem.ShowAsActionCollapseActionView);


			return true;
		}

		public override bool OnOptionsItemSelected(Xamarin.ActionbarSherlockBinding.Views.IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString();

			switch (itemTitle)
			{

			case "Favorites":
				item.SetTitle ("All");
				favorites = true;
				LoadFavorites ();
				break;

			case "All":
				item.SetTitle ("Favorites");
				LoadAll ();
				favorites = false;
				break;
			}

			return base.OnOptionsItemSelected(item);
		}
			
		public bool OnQueryTextSubmit (String query)
		{
			myList = objDb.SearchByName(query);
			lstSongList.Adapter = new DataAdapter(this,myList);
			return true;
		}

		public bool OnQueryTextChange (String newText)
		{
			myList = objDb.SearchByName(newText);
			lstSongList.Adapter = new DataAdapter(this,myList);
			return false;
		}

		public bool OnSuggestionSelect (int position)
		{
			return false;
		}

		public bool OnSuggestionClick (int position)
		{
			Toast.MakeText (this, "Suggestion clicked: ", ToastLength.Long).Show ();
			return true;
		}


		public void ShuffleSong()
		{

			// find favorite Count

			int favCount = objDb.ViewAll().FindAll(p => p.Favorite == "True").ToList().Count();

			if (favCount > 10) {
				// select from the favorite list

				Random r = new Random ();
				int songno = r.Next (0, favCount - 1);
				OpenSong (songno);

			} else {
				// select from all list
				int allCount = objDb.ViewAll ().Count();

				Random r = new Random ();
				int songno = r.Next (0, allCount - 1);
				OpenSong (songno);
			}

		}

		public void OpenSong(int position)
		{
			var SongItem = myList[position];
			var fullsong = new Intent (this, typeof(FullSong));

			fullsong.PutExtra ("SongId",SongItem.SongId);
			fullsong.PutExtra ("SongTitle", SongItem.SongName );
			fullsong.PutExtra ("SongLyrics", SongItem.Lyrics);
			fullsong.PutExtra ("Favorite",SongItem.Favorite);

			StartActivity (fullsong);
		}

	}
}


