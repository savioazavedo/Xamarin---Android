using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RadioButton
{
	[Activity (Label = "RadioButton", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{




		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var rbMale = FindViewById<RadioButton> (Resource.Id.rbMale);
			var rbFemale = FindViewById<RadioButton> (Resource.Id.rbFemale);


			
		}




	}
}


