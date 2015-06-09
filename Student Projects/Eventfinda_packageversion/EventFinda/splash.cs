
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
using System.Threading;

namespace EventFinda
{
	[Activity (Label = "Eventure", MainLauncher = true,NoHistory= true, Theme = "@style/Theme.Splash", Icon = "@drawable/ic_launcher", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class splash : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Thread.Sleep (2500);
			StartActivity (typeof(MainActivity));
			// Create your application here
		}
	}
}

