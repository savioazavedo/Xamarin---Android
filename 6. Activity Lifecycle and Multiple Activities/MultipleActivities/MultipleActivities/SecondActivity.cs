
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
		}
	}
}

