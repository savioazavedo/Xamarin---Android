
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
	[Activity (Label = "ViewNotes")]			
	public class ViewNotes : Activity
	{
		DatabaseManager objDb;
		ListView lstNotes;
		List<Note> myList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ViewNotes);
			lstNotes = FindViewById<ListView> (Resource.Id.lstNotes);
			lstNotes.ItemClick += OnLstNotesClick;

			objDb = new DatabaseManager();
			ShowNotes ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			ShowNotes ();
		}

		public void ShowNotes()
		{
			myList  = objDb.ViewAllNotes();
			lstNotes.Adapter = new DataAdapter(this,myList);
		}

		void OnLstNotesClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var Note = myList[e.Position];
			var editnotes = new Intent (this, typeof(EditNotes));

			editnotes.PutExtra ("Title", Note.Title);
			editnotes.PutExtra ("Description", Note.Description);
			editnotes.PutExtra ("NoteID", Note.NoteID);

			StartActivity (editnotes);

		}

	}
}

