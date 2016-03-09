using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActivityLifeCycle
{
	[Activity (Label = "ActivityLifeCycle", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			Toast.MakeText (this,"On Create",ToastLength.Long).Show ();
		}

		protected override void OnStart()
		{
			base.OnStart ();
			Toast.MakeText (this,"On Start",ToastLength.Long).Show ();
		}

		protected override void OnResume()
		{
			base.OnResume ();
			Toast.MakeText (this,"On Resume",ToastLength.Long).Show ();
		}

		protected override void OnRestart()
		{
			base.OnRestart ();
			Toast.MakeText (this, "On Restart", ToastLength.Long).Show ();
		}

		protected override void OnPause()
		{
			base.OnPause ();
			Toast.MakeText (this, "On Pause", ToastLength.Long).Show ();
		}

		protected override void OnStop()
		{
			base.OnStop ();
			Toast.MakeText (this, "On Stop", ToastLength.Long).Show ();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy ();
			Toast.MakeText (this, "On Destroy", ToastLength.Long).Show ();
		}

	}
}


