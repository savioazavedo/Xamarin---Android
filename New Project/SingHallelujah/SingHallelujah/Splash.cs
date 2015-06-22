
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

namespace SingHallelujah
{
	[Activity (Label = "Sing Hallelujah", MainLauncher = true,NoHistory= true, Theme = "@style/Theme.Splash",Icon = "@drawable/icon")]				
	public class Splash : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Thread.Sleep (2000);
			StartActivity (typeof(MainActivity));

		}
	}
}

