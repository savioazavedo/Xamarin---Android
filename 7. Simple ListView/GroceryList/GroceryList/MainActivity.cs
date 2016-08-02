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
        int Position;

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
            lvItems.ItemLongClick += LvItems_ItemLongClick; ;
            //lvItems.ItemClick += LvItems_ItemClick;
		}


        private void LvItems_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

            AlertDialog altDg = builder.Create();
            altDg.SetTitle("Delete");
            altDg.SetIcon(Resource.Drawable.Icon);
            altDg.SetMessage("Do you want to delete this item from your list ?");

            altDg.SetButton("Yes", (s, ev) => {

                listAdapter.Remove(lvItems.GetItemAtPosition(e.Position));
                listAdapter.NotifyDataSetChanged();

            });

            altDg.SetButton2("No", (s, ev) => {
                //Do Something 
            });
            altDg.Show();
        }

        private void LvItems_LongClick(object sender, View.LongClickEventArgs e)
        {

          
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

			for (var i = 0; i < lvItems.Count; i++) 
			{

				if (selectedItems.ValueAt (i) == true && selectedItems.Size() < lvItems.Count)
                {
					listAdapter.Remove (lvItems.GetItemAtPosition (selectedItems.KeyAt (i)));
                    listAdapter.NotifyDataSetChanged();
				}
			}

            lvItems.ClearChoices();
		}


	}
}


