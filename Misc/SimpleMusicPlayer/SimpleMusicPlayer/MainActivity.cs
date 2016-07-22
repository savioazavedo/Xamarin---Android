using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using Android.Content.Res;

namespace SimpleMusicPlayer
{
    [Activity(Label = "SimpleMusicPlayer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        MediaPlayer player;
        ImageButton btnPlay;
        SeekBar songProgressBar;
        TextView songCurrent;
        TextView songTotal;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.player);

            // Get our button from the layout resource,
            // and attach an event to it
            btnPlay = FindViewById<ImageButton>(Resource.Id.btnPlay);
            songCurrent = FindViewById<TextView>(Resource.Id.songCurrentDurationLabel);
            songTotal = FindViewById<TextView>(Resource.Id.songTotalDurationLabel);

            songProgressBar = FindViewById<SeekBar>(Resource.Id.songProgressBar);
            btnPlay.Click += BtnPlay_Click;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            playSong();
        }

        public void playSong()
        {
            Utilities utils = new Utilities();

            player = MediaPlayer.Create(this, Resource.Raw.Dancing);

            player.Start();

            btnPlay.SetBackgroundResource(Resource.Drawable.img_btn_pause);

            songProgressBar.Progress= 0;
            songProgressBar.Max = 100;

            songTotal.Text = utils.Msectotime(player.Duration);

            CountDown();
        }

        private void CountDown()
        {

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;


        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            songProgressBar.Progress += 1;
        }

    }
}

