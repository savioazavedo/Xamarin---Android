using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using System.Collections.Generic;

namespace Userprofile2
{
    [Activity(Label = "Userprofile2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        ListView lvContacts;
        List<Contact> lstContacts;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lvContacts = FindViewById<ListView>(Resource.Id.lvContacts);
             
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;

            string[] projection = {
                                    ContactsContract.Contacts.InterfaceConsts.Id,
                                    ContactsContract.CommonDataKinds.Identity.InterfaceConsts.DisplayName,
                                    ContactsContract.CommonDataKinds.Phone.Number,
                                    ContactsContract.CommonDataKinds.Email.Address,
                                    ContactsContract.Contacts.InterfaceConsts.PhotoThumbnailUri,
                                  };

            var cursor = ManagedQuery(uri, projection, null, null, null);

            var contactList = new List<string>();
            lstContacts = new List<Contact>();

            if (cursor.MoveToFirst())
            {
                do
                {

                    var name = cursor.GetString(cursor.GetColumnIndex(projection[1]));
                    var phone = cursor.GetString(cursor.GetColumnIndex(projection[2]));
                    var email = cursor.GetString(cursor.GetColumnIndex(projection[3]));
                    var photo = cursor.GetString(cursor.GetColumnIndex(projection[4]));

                    Contact c = new Contact();

                    c.Name = name;
                    c.Phone = phone;
                    c.Email = email;
                    c.Photo = photo;

                    lstContacts.Add(c);
                    contactList.Add(name + " \n " + phone);

                } while (cursor.MoveToNext());
            }

            lvContacts.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, contactList);
            lvContacts.ItemClick += LvContacts_ItemClick;
        }

        private void LvContacts_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Contact temp = lstContacts[e.Position];

            var UserProfile = new Intent(this, typeof(ProfileActivity));

            UserProfile.PutExtra("Name", temp.Name);
            UserProfile.PutExtra("Phone", temp.Phone);
            UserProfile.PutExtra("Email", temp.Email);
            UserProfile.PutExtra("Photo", temp.Photo);

            StartActivity(UserProfile);

        }
    }
}

