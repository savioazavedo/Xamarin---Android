
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

namespace MultipleActivities
{
	[Activity (Label = "SecondActivity")]			
	public class SecondActivity : Activity
	{
		TextView txtMessage;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Second);

			txtMessage = FindViewById<TextView> (Resource.Id.txtMessage);
			txtMessage.Text = "Hi " + Intent.GetStringExtra ("Name");

            int ID = Intent.GetIntExtra("ID", 0);
            string Course = Intent.GetStringExtra("Course");

            Toast.MakeText(this, "ID:" + ID, ToastLength.Long).Show();
            Toast.MakeText(this, "Course:" + Course, ToastLength.Long).Show();

        }
    }
}

