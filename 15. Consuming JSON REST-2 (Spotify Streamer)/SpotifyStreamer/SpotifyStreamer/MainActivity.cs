using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;


namespace SpotifyStreamer
{
    [Activity(Label = "SpotifyStreamer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        SearchView svSongSearch;
        RESThandler objRest;
        ListView lstSong;
        List<Item> SongList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            svSongSearch = FindViewById<SearchView>(Resource.Id.svSong);
            lstSong = FindViewById<ListView>(Resource.Id.lstSong);

            svSongSearch.QueryTextSubmit += SvArtistSearch_QueryTextSubmit;
            lstSong.ItemClick += LstArtist_ItemClick;
        }

        private void LstArtist_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var Song = SongList[e.Position];
            var TracksActivity = new Intent(this, typeof(TracksActivity));
            TracksActivity.PutExtra("URL", Song.preview_url);
            TracksActivity.PutExtra("Image", Song.album.images[0].url);
            StartActivity(TracksActivity);
        }

        private async void SvArtistSearch_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            objRest = new RESThandler(@"https://api.spotify.com/v1/search?q=" + svSongSearch.Query + "&type=track&market=nz");
            var Response = await objRest.ExecuteRequestAsync();

            lstSong.Adapter = new DataAdapter(this, Response.tracks.items);
            SongList = Response.tracks.items;
        }




    }
}

