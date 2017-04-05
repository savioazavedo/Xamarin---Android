using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MultipleActivities
{
	[Activity (Label = "MultipleActivities", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button btnNext;
		TextView txtName;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			txtName = FindViewById<TextView> (Resource.Id.txtName);
			btnNext = FindViewById<Button> (Resource.Id.btnNext);
			btnNext.Click += OnNextButtonClick;
		}

		public void OnNextButtonClick(object sender,EventArgs e)
		{
			var activity2 = new Intent (this, typeof(SecondActivity));

			activity2.PutExtra ("Name", txtName.Text);
            activity2.PutExtra ("ID", 101);
            activity2.PutExtra ("Course","Accounts");

            StartActivity (activity2);
			
		}

	}
}


