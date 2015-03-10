using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace OptionsMenusDemo
{
	[Activity (Label = "OptionsMenusDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		WebView web_view;

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
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			web_view = FindViewById<WebView> (Resource.Id.webView1);
			web_view.SetWebViewClient (new HelloWebViewClient ());
			web_view.Settings.JavaScriptEnabled = true;
			web_view.LoadUrl ("http://www.google.com");
		}


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			menu.Add("Twitter");
			menu.Add("Facebook");
			menu.Add("Instagram");
			return base.OnPrepareOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString();

			switch (itemTitle)
			{
			case "Twitter":
				web_view.LoadUrl("http://www.twitter.com");
				break;
			case "Facebook":
				web_view.LoadUrl("http://www.facebook.com");
				break;
			case "Instagram":
				web_view.LoadUrl ("http://www.instagram.com");
				break;
			}

			return base.OnOptionsItemSelected(item);
		}
			
	}
}


