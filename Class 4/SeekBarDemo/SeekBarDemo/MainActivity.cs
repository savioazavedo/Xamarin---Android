using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SeekBarDemo
{
	[Activity (Label = "SeekBarDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		SeekBar skSeekBar;
		TextView txtValue;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//Initialize controls 
			skSeekBar = FindViewById<SeekBar> (Resource.Id.skSeekBar);
			txtValue = FindViewById<TextView> (Resource.Id.txtValue);

			skSeekBar.ProgressChanged += OnSeekBarChange;

		}

		private void OnSeekBarChange(object sender, EventArgs e)
		{
			txtValue.Text = Convert.ToString(skSeekBar.Progress);
		}
			
	}
}


