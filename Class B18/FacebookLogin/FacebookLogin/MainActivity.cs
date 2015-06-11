using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook.Widget;
using Xamarin.Facebook;

[assembly:Permission (Name = Android.Manifest.Permission.Internet)]
[assembly:Permission (Name = Android.Manifest.Permission.WriteExternalStorage)]
[assembly:MetaData ("com.facebook.sdk.ApplicationId", Value ="@string/app_id")]

namespace FacebookLogin
{
	[Activity (Label = "FacebookLogin", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, Session.IStatusCallback, Request.IGraphUserCallback
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			Session.OpenActiveSession (this, true, this);

		}
			
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			// Relay the result to our FB Session
			Session.ActiveSession.OnActivityResult (this, requestCode, (int)resultCode, data);

		}

		public void Call (Session session, SessionState state, Java.Lang.Exception exception)
		{
			// Make a request for 'Me' information about the current user
			if (session.IsOpened)
				Request.ExecuteMeRequestAsync (session, this);
		}

		public void OnCompleted (Xamarin.Facebook.Model.IGraphUser user, Response response)
		{
			// 'Me' request callback
			Toast.MakeText (this, "Got User", ToastLength.Long).Show ();

			if (user != null)
				Console.WriteLine ("GOT USER: " + user.FirstName);
			else
				Console.WriteLine ("Failed to get 'me'!");

			StartActivity (typeof(UserDetails));

		}


	}
}

      
