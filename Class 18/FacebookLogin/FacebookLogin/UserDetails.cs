
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
using Xamarin.Facebook;
using Xamarin.Facebook.Widget;


namespace FacebookLogin
{
	[Activity (Label = "UserDetails")]			
	public class UserDetails : Activity, Session.IStatusCallback,Request.IGraphUserCallback
	{

		TextView txtDetails;
		ProfilePictureView profilePic;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.UserProfile);

			profilePic = FindViewById<ProfilePictureView> (Resource.Id.profilePicture);
			txtDetails = FindViewById<TextView> (Resource.Id.txtDetails);

			Session.OpenActiveSession (this, true, this);

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
			profilePic.ProfileId = user.Id;
			txtDetails.Text = user.FirstName + "\n" + user.LastName + "\n" + user.Username;
		}
	}
}

