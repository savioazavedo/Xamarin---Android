
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

namespace SingHallelujah
{
	[Activity (Label = "FullSong")]			
	public class FullSong : Activity
	{

		int SongId;
		string SongTitle;
		string SongLyrics;
		TextView txtSongTitle;
		TextView txtSongLyrics;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.FullSong);

			txtSongTitle = FindViewById<TextView> (Resource.Id.txtSongTitle);
			txtSongLyrics = FindViewById<TextView> (Resource.Id.txtSongLyrics);


			SongTitle = Intent.GetStringExtra("SongTitle");
			SongLyrics = Intent.GetStringExtra("SongLyrics");

			txtSongTitle.Text = SongTitle;
			txtSongLyrics.Text = SongLyrics;

		}
	}
}

