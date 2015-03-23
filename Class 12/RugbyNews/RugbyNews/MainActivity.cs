using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace RugbyNews
{
	[Activity (Label = "RugbyNews", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RESThandler objRest;

		ListView lstNews;
		List<Item> tmpNewsList; 

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			lstNews = FindViewById<ListView> (Resource.Id.lstNews);

			lstNews.ItemClick += OnLstNewsClick;

			LoadRugbyNews ();
		}

		void OnLstNewsClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var NewsItem = tmpNewsList[e.Position];
			//Toast.MakeText (this, NewsItem.Link, ToastLength.Short).Show (); 


			// start a new activity and pass the NewsItem.Link as an Intent

			var NewsArticle = new Intent (this, typeof(Article));
			NewsArticle.PutExtra ("URL", NewsItem.Link);
			StartActivity (NewsArticle);

		}
			

		public async void LoadRugbyNews()
		{
			objRest = new RESThandler (@"http://rss.nzherald.co.nz/rss/xml/nzhrsscid_000000080.xml");
			var Response = await objRest.ExecuteRequestAsync ();

			lstNews.Adapter = new DataAdapter (this, Response.Channel.Item);
			tmpNewsList = Response.Channel.Item;
		}

	}
}


