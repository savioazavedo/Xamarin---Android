using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RugbyNews
{
	[Activity (Label = "RugbyNews", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RESThandler objRest;

		ListView lstNews;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			lstNews = FindViewById<ListView> (Resource.Id.lstNews);

			LoadRugbyNews ();
		}

		public async void LoadRugbyNews()
		{
			objRest = new RESThandler (@"http://rss.nzherald.co.nz/rss/xml/nzhrsscid_000000080.xml");
			var Response = await objRest.ExecuteRequestAsync ();

			lstNews.Adapter = new DataAdapter (this, Response.Channel.Item);
		}

	}
}


