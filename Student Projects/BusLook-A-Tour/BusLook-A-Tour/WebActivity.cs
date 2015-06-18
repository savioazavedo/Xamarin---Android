
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

namespace BusLookATour
{
	[Activity (Label = "WebActivity")]			
	public class WebActivity : Activity
	{

		WebView web_view;

		public class HelloWebViewClient : WebViewClient
		{
	
			public override bool ShouldOverrideUrlLoading(WebView view, string url)
			{
				view.LoadUrl (url);
				return true;
			}
		}


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Webview);
			web_view = FindViewById<WebView> (Resource.Id.webview);
			web_view.SetWebViewClient (new HelloWebViewClient ());
			web_view.Settings.JavaScriptEnabled = true;
			web_view.LoadUrl (Intent.GetStringExtra("url"));

			// Create your application here
		}
	}
}

