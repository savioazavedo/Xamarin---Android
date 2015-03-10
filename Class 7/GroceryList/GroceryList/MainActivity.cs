using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GroceryList
{
	[Activity (Label = "GroceryList", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		AutoCompleteTextView acItem;
		Button btnItemAdd;
		Button btnRemove;
		ListView lvItems;
		ArrayAdapter listAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			//Intializing the grocery list for the autocomplete textbox
			var grocerylist_array = Resources.GetStringArray (Resource.Array.grocerylist);
			acItem = FindViewById<AutoCompleteTextView> (Resource.Id.acItem);
			acItem.Adapter = new ArrayAdapter<String> (this,Android.Resource.Layout.TestListItem,grocerylist_array);

			//Initialising the controls and click event
			btnItemAdd = FindViewById<Button> (Resource.Id.btnAdd);
			lvItems = FindViewById<ListView> (Resource.Id.lvItems);
			btnItemAdd.Click += OnItemAddClick;

			// Initialising a list and binding it to an adapter
			lvItems.ChoiceMode = ChoiceMode.Multiple;
			listAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItemMultipleChoice);
			lvItems.Adapter = listAdapter;

			//
			btnRemove = FindViewById<Button> (Resource.Id.btnRemove);
			btnRemove.Click += RemoveSelectedItems;
		}

		public void OnItemAddClick(object sender,EventArgs e)
		{
			if (acItem.Text.Length > 0)
			{
				listAdapter.Add (acItem.Text);
				acItem.Text = "";
			}
		}

		public void RemoveSelectedItems(object sender,EventArgs e)
		{
			var selectedItems = FindViewById<ListView>(Resource.Id.lvItems).CheckedItemPositions;

			for (var i = 0; i < selectedItems.Size(); i++) 
			{

				if (selectedItems.ValueAt (i) == true && selectedItems.Size() <= lvItems.Count) {
					listAdapter.Remove (lvItems.GetItemAtPosition (selectedItems.KeyAt (i)));
				}
			}
		}


	}
}


