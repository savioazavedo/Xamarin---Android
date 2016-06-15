using System;
using AndroidHUD;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;

namespace WordoftheDay
{
	[Activity (Label = "WordoftheDay", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : Activity
	{

		TextView txtword;
		TextView txtDescription;
		RESThandler objRest;

        

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		
			txtword = FindViewById<TextView> (Resource.Id.txtWord);
			txtDescription = FindViewById<TextView> (Resource.Id.txtDescription);

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            LoadTodaysWordAsync();

            
        }

		public void LoadTodaysWord()
		{
			objRest = new RESThandler (@"https://www.wordsmith.org/awad/rss1.xml");
			var Response = objRest.ExecuteRequest();
			txtword.Text = Response.Channel.Item[0].Title;
			txtDescription.Text = Response.Channel.Item[0].Description;
		}

		public async void LoadTodaysWordAsync()
		{
			//AndHUD.Shared.Show(this, "Loading ...", 30);

			objRest = new RESThandler (@"https://www.wordsmith.org/awad/rss1.xml");
			var Response = await objRest.ExecuteRequestAsync ();
			txtword.Text = Response.Channel.Item[0].Title;
			txtDescription.Text = Response.Channel.Item[0].Description;

			//AndHUD.Shared.Dismiss (this);
		}
	}
}
