
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

namespace EventFinda
{
	[Activity (Label = "SearchEvent", Icon = "@drawable/ic_launcher",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]			
	public class SearchEvent : Activity
	{


		Button btnFree;
		Button btnpaid;
		EditText SearchItem;
		ListView SearchLocation;
		ListView SearchCategory;
		Button Search;

		ArrayAdapter listAdapter;
		AutoCompleteTextView acItem;
		AutoCompleteTextView acItem1;




		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Search);
			btnFree = FindViewById <Button> (Resource.Id.btnFree);
			btnpaid = FindViewById<Button> (Resource.Id.btnPaid);
			SearchItem = FindViewById<EditText> (Resource.Id.txtEventSearch);

			SearchLocation = FindViewById<ListView> (Resource.Id.lstLocation);
			SearchCategory = FindViewById<ListView> (Resource.Id.lstCategory);

			var location_array = Resources.GetStringArray (Resource.Array.location);
			var category_array = Resources.GetStringArray (Resource.Array.category);

			listAdapter = new ArrayAdapter <string> (this, Android.Resource.Layout.SimpleListItemSingleChoice);
			acItem = FindViewById <AutoCompleteTextView> (Resource.Id.txtLocationSearch);
			acItem.Adapter = new ArrayAdapter <string> (this, Android.Resource.Layout.TestListItem, location_array);

			acItem1 = FindViewById<AutoCompleteTextView> (Resource.Id.txtCategorySearch);
			acItem1.Adapter= new ArrayAdapter <string> (this, Android.Resource.Layout.TestListItem, category_array);

			SearchLocation.ChoiceMode = ChoiceMode.Single;
			listAdapter = new ArrayAdapter <string> (this, Android.Resource.Layout.SimpleListItemMultipleChoice);
			SearchLocation.Adapter = listAdapter;
			SearchCategory.Adapter = listAdapter;

			Search = FindViewById<Button> (Resource.Id.btnSearch);





			Search.Click += onbtnSearchClick;
			btnFree.Click += onbtnFreeClick;
			btnpaid.Click += onbtnPaidClick;
			// Create your application here
		}
		public void onbtnFreeClick (object sender, EventArgs e)
		{
			StartActivity (typeof(FreeList));
		}
		public void onbtnPaidClick(object sender, EventArgs e)
			{
			StartActivity (typeof(PaidList));

			}
		public void onbtnSearchClick(object sender, EventArgs e)
		{
			if ((SearchItem.Text != "") || (acItem.Text !="") || (acItem1.Text != "")) {
				if (SearchItem.Text != "") {
					var SearchEventbyNameList = new Intent (this, typeof(SearchEventbyName));
					SearchEventbyNameList.PutExtra ("SearchName", SearchItem.Text);
					StartActivity (SearchEventbyNameList);
				} else if (acItem.Text != "") {
										var SearchEventbyLocationList = new Intent (this, typeof(SearchLocation));
					SearchEventbyLocationList.PutExtra ("SearchLocations", acItem.Text);
					StartActivity (SearchEventbyLocationList);
				}
				else if (acItem1.Text != "" ) 
				{
					var SearchEventbyCategoryList = new Intent (this, typeof(SearchCategory));
					SearchEventbyCategoryList.PutExtra ("SearchCategory", acItem1.Text);
					StartActivity (SearchEventbyCategoryList);
				}
							}
					else {
				Toast.MakeText (this, "Please enter one of textbox partially to search", ToastLength.Long).Show ();
					}


		}
	}
}

