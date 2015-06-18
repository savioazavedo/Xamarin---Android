using System;

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


namespace SingHallelujah
{
	[Activity (Label = "Sing Hallelujah", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : SherlockActivity,SearchView.IOnQueryTextListener,SearchView.IOnSuggestionListener
	{
		//int count = 1;

		ListView lstSongList;
		List<Song> myList;
		static string dbName = "SingHalleluiah.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);
		DatabaseManager objDb = new DatabaseManager();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetTheme (Resource.Style.Theme_Sherlock);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			CopyDatabase ();

			myList = objDb.ViewAll();

			lstSongList = FindViewById <ListView>(Resource.Id.lstSongs);
			lstSongList.Adapter = new DataAdapter(this,myList);
			lstSongList.ItemClick += lstSongListClick; 
		}

		void lstSongListClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var SongItem = myList[e.Position];
			var fullsong = new Intent (this, typeof(FullSong));

			fullsong.PutExtra ("SongTitle", SongItem.SongName );
			fullsong.PutExtra ("SongLyrics", SongItem.Lyrics);

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

			} catch (Exception ex){


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

			return true;
		}


		public bool OnQueryTextSubmit (String query)
		{
			Toast.MakeText (this, "You searched for: " + query, ToastLength.Long).Show ();
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
	}
}


