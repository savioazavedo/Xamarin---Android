
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
using Android.Webkit;

namespace RugbyNews
{
	[Activity (Label = "Article")]			
	public class Article : Activity
	{

		String Url;
		WebView wvNews;

		public class HelloWebViewClient : WebViewClient
		{
			public override bool ShouldOverrideUrlLoading (WebView view, string url)
			{
				view.LoadUrl (url);
				return true;
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.NewsArticle);

			Url = Intent.GetStringExtra("URL");

			wvNews = FindViewById<WebView> (Resource.Id.webView1);

			wvNews.SetWebViewClient (new HelloWebViewClient ());
			wvNews.Settings.JavaScriptEnabled = true;

			wvNews.LoadUrl (Url);

			Toast.MakeText (this, Url, ToastLength.Short).Show (); 

		}
	}
}

