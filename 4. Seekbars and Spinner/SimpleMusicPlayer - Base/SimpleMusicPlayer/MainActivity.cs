using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.Timers;

namespace SimpleMusicPlayer
{
    [Activity(Label = "SimpleMusicPlayer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {



        int count = 1;
        ImageButton btnPlay;
        TextView songTotalDurationLabel;
        SeekBar songProgress;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.player);

            // Get our button from the layout resource,
            // and attach an event to it
            btnPlay = FindViewById<ImageButton>(Resource.Id.btnPlay);
            songTotalDurationLabel = FindViewById<TextView>(Resource.Id.songTotalDurationLabel);
            songProgress = FindViewById<SeekBar>(Resource.Id.songProgressBar);

            btnPlay.Click += BtnPlay_Click;

           // button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            MediaPlayer player;
            player = MediaPlayer.Create(this, Resource.Raw.Dancing);
            player.Start();

           
            double time,minutes;
            int minint,seconds,onlyseconds;

            time = player.Duration;

           

            minutes = (time / (1000 * 60));

            minint = (int)(minutes);

            seconds = (int)((time % (1000*60*60)) % (1000 * 60) /1000);

            songTotalDurationLabel.Text = minint + "." + seconds;

            onlyseconds = (int)((time / (1000)));
            songProgress.Max = onlyseconds;

            Timer timer = new Timer(1000); 
            timer.Start();
            timer.Elapsed += Timer_Elapsed; 

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            RunOnUiThread(() => songProgress.Progress += 1);
        }
    }
}

