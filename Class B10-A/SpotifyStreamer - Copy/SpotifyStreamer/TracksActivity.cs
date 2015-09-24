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
using Android.Media;
using System.Net;
using Android.Graphics;

namespace SpotifyStreamer
{
    [Activity(Label = "TracksActivity")]
    public class TracksActivity : Activity
    {

        RESThandler objRest;
     
        ImageView albumart; 

        private string Mp3;
        private string ArtUrl;
        private MediaPlayer player;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Track);
            Mp3 = Intent.GetStringExtra("URL");
            ArtUrl = Intent.GetStringExtra("Image");

            albumart = FindViewById<ImageView>(Resource.Id.AlbumArt);

            var imageBitmap = GetImageBitmapFromUrl(ArtUrl);
            albumart.SetImageBitmap(imageBitmap);

            LoadTrack();
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;
            if (!(url == "null"))
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }

            return imageBitmap;
        }



        public void LoadTrack()
        {
            Play();
        }

        private void IntializePlayer()
        {
            player = new MediaPlayer();

            //Tell our player to sream music
            player.SetAudioStreamType(Stream.Music);
        }

        private async void Play()
        {
            if (player == null)
            {
                IntializePlayer();
            }

            try
            {
                await player.SetDataSourceAsync(ApplicationContext, Android.Net.Uri.Parse(Mp3));
                player.PrepareAsync();
                player.Prepared += Player_Prepared;
            }
            catch (Exception ex)
            {
                //unable to start playback log error
                Console.WriteLine("Unable to start playback: " + ex);
            }
        }

        private void Player_Prepared(object sender, EventArgs e)
        {
            player.Start();
        }
    }
}