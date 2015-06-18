
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
	[Activity (Label = "ContactActivity")]			
	public class ContactActivity : Activity
	{
		Button btnFacebook;
		Button btnCall;

		Button btnComplaint;
		Button btnCompliment;
		Button btnQuestion;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Contact);

			btnFacebook = FindViewById <Button> (Resource.Id.btnFacebook);

			btnCall = FindViewById <Button> (Resource.Id.btnCall);
			btnComplaint = FindViewById <Button> (Resource.Id.btnComplaint);
			btnCompliment = FindViewById <Button> (Resource.Id.btnCompliment);
			btnQuestion = FindViewById <Button> (Resource.Id.btnQuestion);

			btnFacebook.Click += OnBtnFacebookClick;
			btnCall.Click += OnDialButtonClick;
			btnComplaint.Click += OnComplaintBtnClick;
			btnCompliment.Click += OnComplimentBtnClick;
			btnQuestion.Click += OnQuestionBtnClick;
		}

		public void OnBtnFacebookClick(object sender,EventArgs e)
		{
			var uri = Android.Net.Uri.Parse ("https://www.facebook.com/BUSITWaikato");
			var intent = new Intent (Intent.ActionView, uri); 
			StartActivity (intent);   
		}

		public void OnDialButtonClick(object sender,EventArgs e)
		{
			var uri = Android.Net.Uri.Parse ("tel:" + "0800 4 2875 463");
			var intent = new Intent (Intent.ActionView, uri);
			StartActivity (intent);
		}

//		void OnComplaintBtnClick (object sender, EventArgs e)
//		{
//			SetContentView (Resource.Layout.Webview);
//
////			web_view = FindViewById<WebView> (Resource.Id.webview);
////			web_view.Settings.JavaScriptEnabled = true;
////			web_view.LoadUrl ("http://busit.co.nz/mobile/About-us/Contact-us/Compliment-form/");
//		}
//
//		void OnComplimentBtnClick (object sender, EventArgs e)
//		{
//			SetContentView (Resource.Layout.Webview);
//
////			web_view = FindViewById<WebView> (Resource.Id.webview);
////			web_view.Settings.JavaScriptEnabled = true;
////			web_view.LoadUrl ("http://busit.co.nz/mobile/About-us/Contact-us/Compliment-form/");
//		}
//
//		void OnQuestionBtnClick (object sender, EventArgs e)
//		{
//			SetContentView (Resource.Layout.Webview);
//
////			web_view = FindViewById<WebView> (Resource.Id.webview);
////			web_view.Settings.JavaScriptEnabled = true;
////			web_view.LoadUrl ("http://busit.co.nz/mobile/About-us/Contact-us/Question-form/");
//		}

		public void OnComplaintBtnClick (object sender, EventArgs e)
		{
			var activity2 = new Intent (this, typeof(WebActivity));
			activity2.PutExtra("url","http://busit.co.nz/mobile/About-us/Contact-us/Compliment-form/");
			StartActivity (activity2);
		}

		public void OnComplimentBtnClick (object sender, EventArgs e)
		{
			var activity2 = new Intent (this, typeof(WebActivity));
			activity2.PutExtra("url","http://busit.co.nz/mobile/About-us/Contact-us/Compliment-form/");
			StartActivity (activity2);
		}

		public void OnQuestionBtnClick (object sender, EventArgs e)
		{
			var activity2 = new Intent (this, typeof(WebActivity));
			activity2.PutExtra("url","http://busit.co.nz/mobile/About-us/Contact-us/Question-form/");
			StartActivity (activity2);
		}

	}
}

