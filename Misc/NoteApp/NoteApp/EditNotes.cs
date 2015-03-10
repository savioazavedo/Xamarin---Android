
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
	[Activity (Label = "EditNotes")]			
	public class EditNotes : Activity
	{
		int NoteId;
		string Title;
		string Description;

		TextView txtTitle;
		TextView txtDescription;
		Button btnEdit;
		Button btnDelete;
		DatabaseManager objDb;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.EditNote);

			txtTitle = FindViewById<TextView> (Resource.Id.TxtEditTitle);
			txtDescription = FindViewById<TextView> (Resource.Id.txtEditDescription);
			btnEdit = FindViewById<Button> (Resource.Id.btnEdit);
			btnDelete = FindViewById<Button> (Resource.Id.btnDelete);

			NoteId = Intent.GetIntExtra("NoteID",0);
			Description = Intent.GetStringExtra("Description");
			Title = Intent.GetStringExtra("Title");

			txtTitle.Text = Title;
			txtDescription.Text = Description;

			btnEdit.Click += OnBtnEditClick;
			btnDelete.Click += OnBtnDeleteClick;

			objDb = new DatabaseManager();

		}
			
		public void OnBtnEditClick(object sender, EventArgs e)
		{
			try 
			{
				objDb.EditNote(txtTitle.Text,txtDescription.Text,NoteId);
				Toast.MakeText(this,"Note Added",ToastLength.Long).Show();
			} catch (Exception ex) {
				Console.WriteLine ("Error Occurred:" + ex.Message);
			}
		}

		public void OnBtnDeleteClick(object sender, EventArgs e)
		{
			try
			{
				objDb.DeleteNote(NoteId);
				Toast.MakeText(this,"Note Deleted",ToastLength.Long).Show();
				StartActivity(typeof(ViewNotes));
			} catch (Exception ex) {
				Console.WriteLine ("Error Occurred:" + ex.Message);
			}
				
		}

	}
}

