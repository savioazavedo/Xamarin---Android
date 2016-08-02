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
using Android.Graphics;
using Android.Provider;

namespace Userprofile2
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {

        TextView txtUsername;
        TextView txtName;
        TextView txtPhone;
        TextView txtEmail;
        ImageButton imgProfilePic;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.UserProfile);

            var name = Intent.GetStringExtra("Name");
            var phone = Intent.GetStringExtra("Phone");
            var email = Intent.GetStringExtra("Email");
            var photo = Intent.GetStringExtra("Photo");

            txtUsername = FindViewById<TextView>(Resource.Id.user_profile_name);
            txtName = FindViewById<TextView>(Resource.Id.textView1);
            txtPhone = FindViewById<TextView>(Resource.Id.textView2);
            txtEmail = FindViewById<TextView>(Resource.Id.textView3);
            imgProfilePic = FindViewById<ImageButton>(Resource.Id.user_profile_photo);

            txtName.Text = name;
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtUsername.Text = name;

            imgProfilePic.SetImageURI(Android.Net.Uri.Parse(photo));

        }
    }
}