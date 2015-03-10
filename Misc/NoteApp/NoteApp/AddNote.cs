
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

namespace NoteApp
{
	[Activity (Label = "AddNote")]			
	public class AddNote : Activity
	{

		TextView txtTitle;
		TextView txtDescription;
		Button btnAdd;
		DatabaseManager objDb;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AddNote);

			txtTitle = FindViewById<TextView>(Resource.Id.txtTitle);
			txtDescription =  FindViewById<TextView>(Resource.Id.txtDescription);
			btnAdd = FindViewById<Button> (Resource.Id.btnAdd);

			btnAdd.Click += OnAddNoteClick;
			objDb = new DatabaseManager ();
		}

		public void OnAddNoteClick(object sender, EventArgs e)
		{
			try
			{
				objDb.AddNote (txtTitle.Text, txtDescription.Text);
				Toast.MakeText(this,"Note Added",ToastLength.Long).Show();
			
			} catch(Exception ex) {

				Console.WriteLine ("Error:" + ex.Message);
			}
		}
	}
}

