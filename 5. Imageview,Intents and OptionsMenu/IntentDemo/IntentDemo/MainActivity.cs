using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace IntentDemo
{
	[Activity (Label = "IntentDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

        Button btnMap;
        Button btnCall;
        Button btnEmail;

        EditText txtPhone;
        EditText txtLat;
        EditText txtLong;
        EditText txtEmailTo;
        EditText txtEmailSubject;
        EditText txtEmailMessage;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnMap = FindViewById<Button> (Resource.Id.btnMap);
            btnCall = FindViewById<Button>(Resource.Id.btnCall);
            btnEmail = FindViewById<Button>(Resource.Id.btnMail);

            txtPhone = FindViewById<EditText>(Resource.Id.txtPhone);
            txtLat = FindViewById<EditText>(Resource.Id.txtLat);
            txtLong = FindViewById<EditText>(Resource.Id.txtLong);
            txtEmailTo = FindViewById<EditText>(Resource.Id.txtEmailTo);
            txtEmailSubject = FindViewById<EditText>(Resource.Id.txtSubject);
            txtEmailMessage = FindViewById<EditText>(Resource.Id.txtMessage);

            btnCall.Click += BtnCall_Click;
            btnMap.Click += BtnMap_Click;
            btnEmail.Click += BtnEmail_Click;

		}

        private void BtnEmail_Click(object sender, EventArgs e)
        {
            var email = new Intent(Android.Content.Intent.ActionSend);

            email.PutExtra(Android.Content.Intent.ExtraEmail, new string[] { txtEmailTo.Text });

            //You can add a CC as well
            //email.PutExtra(Android.Content.Intent.ExtraCc,txtEmailTo.Text);

            email.PutExtra(Android.Content.Intent.ExtraSubject, txtEmailSubject.Text);

            email.PutExtra(Android.Content.Intent.ExtraText,txtEmailMessage.Text);

            email.SetType("message/rfc822");

            StartActivity(email);
        }

        private void BtnMap_Click(object sender, EventArgs e)
        {
            var geoUri = Android.Net.Uri.Parse("geo:"+ txtLat.Text + "," + txtLong.Text);
            var mapIntent = new Intent(Intent.ActionView, geoUri);
            StartActivity(mapIntent);
        }

        private void BtnCall_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("tel:" + txtPhone.Text);
            var intent = new Intent(Intent.ActionDial, uri);
            StartActivity(intent);
        }
    }
}


