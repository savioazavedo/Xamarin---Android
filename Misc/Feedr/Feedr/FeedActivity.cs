
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
using Android.Graphics.Drawables;
using System.IO;
using Android.Graphics;

namespace Feedr
{
	[Activity (Label = "FeedActivity")]			
	public class FeedActivity : Activity
	{

		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Main);




		}

	}
}

