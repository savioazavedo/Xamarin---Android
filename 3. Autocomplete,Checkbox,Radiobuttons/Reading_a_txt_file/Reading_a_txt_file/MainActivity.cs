using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace Reading_a_txt_file
{
	[Activity (Label = "Reading_a_txt_file", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		TextView tv1;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//sets the TextView
			tv1 = FindViewById<TextView> (Resource.Id.textView1);

			//sets the button
			Button button = FindViewById<Button> (Resource.Id.myButton);
            
			button.Click += onbtnclick;
		}

		void onbtnclick (object sender, EventArgs e)
		{
			string content;
			using (StreamReader sr = new StreamReader (Assets.Open ("read_asset.txt")))
			{
				content = sr.ReadToEnd ();
			}

			tv1.Text = content;
		}
	}
}


