using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Contacts;
using Android.Provider;
using System.Collections.Generic;

using Android.Support.V4.Widget;
using Xamarin.ActionbarSherlockBinding;
using Xamarin.ActionbarSherlockBinding.App;
using Xamarin.ActionbarSherlockBinding.Views;
using Xamarin.ActionbarSherlockBinding.Widget;
using SherlockActionBar = Xamarin.ActionbarSherlockBinding.App.ActionBar;
using SearchView = Xamarin.ActionbarSherlockBinding.Widget.SearchView;
using CursorAdapter = Android.Support.V4.Widget.CursorAdapter;
using IMenu = Xamarin.ActionbarSherlockBinding.Views.IMenu;
using System.Text.RegularExpressions;

namespace ContactList
{
	[Activity (Label = "ContactList", MainLauncher = true, Icon = "@drawable/icon")]

	public class MainActivity : SherlockActivity,SearchView.IOnQueryTextListener,SearchView.IOnSuggestionListener
	{
		AddressBook book;
		List<Data> contactList;
		ListView lstContacts;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetTheme (Resource.Style.Theme_Sherlock_Light_DarkActionBar);

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

		public bool OnQueryTextChange (string newText)
		{
			List<Data> TempList;
			TempList = contactList.Where (p => p.Heading.Contains (newText)).ToList ();
			lstContacts.Adapter =new DataAdapter(this,TempList);
			return true;
		}

		public bool OnQueryTextSubmit (string query)
		{
			List<Data> TempList;
			TempList = contactList.Where (p => p.Heading.Contains (query)).ToList ();
			lstContacts.Adapter =new DataAdapter(this,TempList);
			return true;
		}

		public bool OnSuggestionClick (int position)
		{
			throw new NotImplementedException ();
		}
		public bool OnSuggestionSelect (int position)
		{
			throw new NotImplementedException ();
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			//Used to put dark icons on light action bar

			SearchView searchView = new SearchView (SupportActionBar.ThemedContext);
			searchView.QueryHint = "Contact Name";
			searchView.SetOnQueryTextListener (this);
			searchView.SetOnSuggestionListener (this);

			//searchView.SuggestionsAdapter = mSuggestionsAdapter;

			menu.Add ("Search")
				.SetIcon (Resource.Drawable.abs__ic_search)
				.SetActionView (searchView)
				.SetShowAsAction (MenuItem.ShowAsActionIfRoom | MenuItem.ShowAsActionCollapseActionView);
				
			return true;
		}


	}
}


