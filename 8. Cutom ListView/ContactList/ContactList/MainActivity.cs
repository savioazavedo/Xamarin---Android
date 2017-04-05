using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using System.Collections.Generic;
using Xamarin.Contacts;

namespace ContactList
{
	[Activity (Label = "ContactList", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		AddressBook book;
		List<Data> contactList;
		ListView lstContacts;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			book = new AddressBook (this);
			contactList = new List<Data> ();    

			foreach (Contact contact in book) {
				Data d = new Data ();
				d.Heading = contact.DisplayName;

				if (contact.Phones.Count() > 0) {
					d.SubHeading = contact.Phones.ElementAt (0).Number;
				}
				d.ImagePic = contact.GetThumbnail ();
				contactList.Add (d);
			}

			lstContacts = FindViewById<ListView> (Resource.Id.listView1);
			lstContacts.ItemClick += OnlstContactClick;
			lstContacts.Adapter =new DataAdapter(this,contactList);
		}

		public void OnlstContactClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var contact = contactList [e.Position];

			//open up the intent for phone call
			var uri = Android.Net.Uri.Parse ("tel:" + contact.SubHeading);
			var intent = new Intent (Intent.ActionView, uri); 
			StartActivity (intent);

		}

	}
}


