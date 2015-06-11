using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony.Gsm;
using Xamarin.Contacts;

using Android.Provider;
using System.Collections.Generic;

namespace ReadContacts
{
    [Activity (Label = "ReadContacts", MainLauncher = true)]
    public class Activity1 : Activity
    {
		Button btnSend;
		TextView txtMessage;
		ListView lvPhones;
		List<string> contactList;
		AddressBook book;

        protected override void OnCreate (Bundle bundle)
        {
		
            base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
            contactList = new List<string> ();    
			book = new AddressBook (this);

			txtMessage = FindViewById<TextView> (Resource.Id.txtMessage);
			btnSend = FindViewById<Button> (Resource.Id.btnSend);
			lvPhones = FindViewById<ListView> (Resource.Id.lvPhoneList);


			foreach (Contact contact in book) {
				contactList.Add (contact.DisplayName);
			}

			lvPhones.ChoiceMode = ChoiceMode.Multiple;
			lvPhones.Adapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItemMultipleChoice, contactList);

			btnSend.Click += OnbtnSendClick;
        }

		private void OnbtnSendClick(object sender,EventArgs e)
		{
			var selectedContacts = FindViewById<ListView>(Resource.Id.lvPhoneList).CheckedItemPositions;

			for (var i = 0; i < selectedContacts.Size(); i++ )
			{
				var customers = from c in book 
						where c.DisplayName == lvPhones.GetItemAtPosition(selectedContacts.KeyAt(i)).ToString() 
						select c;


				SmsManager.Default.SendTextMessage (customers.ElementAt(0).Phones.ElementAt(0).Number, null,"Hello from Xamarin.Android", null, null);

				var smsUri = Android.Net.Uri.Parse("smsto:" + customers.ElementAt(0).Phones.ElementAt(0).Number);
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				smsIntent.PutExtra ("sms_body", txtMessage.Text);  
				StartActivity (smsIntent);
			}
		}
    }
}


