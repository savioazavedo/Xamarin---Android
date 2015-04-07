
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

namespace ParseToDo
{
	[Activity (Label = "ViewList")]			
	public class ViewList : Activity
	{
		EditText txtToDo;
		Button btnAdd;
		ListView lstItems;
		ParseHandler objParse = ParseHandler.Default;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ListActivity);

			btnAdd = FindViewById<Button> (Resource.Id.btnAdd);
			txtToDo = FindViewById<EditText> (Resource.Id.txtTodo);
			lstItems = FindViewById<ListView> (Resource.Id.lvItems);

			btnAdd.Click += AddToDoItem;
		}

		public async void AddToDoItem (object sender, EventArgs e)
		{

			if (txtToDo.Text != "") {
				var result = await objParse.AddToDoItem (txtToDo.Text);

				if (result == true) {
					Toast.MakeText (this, "Item added successfully", ToastLength.Long).Show ();
				} else {
					Toast.MakeText (this, "Oops something went wrong", ToastLength.Long).Show ();
				}

			}
		}
			


	}
}

