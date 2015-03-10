using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace NoteApp
{
	[Activity (Label = "NoteApp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
	
		Button btnView;
		Button btnAdd;
		static string dbName = "NoteDB.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			btnAdd = FindViewById<Button> (Resource.Id.btnAdd);
			btnView = FindViewById<Button> (Resource.Id.btnView);

			btnAdd.Click += OnBtnAddClick;
			btnView.Click += OnBtnViewClick;
			CopyDatabase ();
		}

		public void CopyDatabase()
		{
			// Check if your DB has already been extracted.
			if (!File.Exists(dbPath))
			{
				using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
				{
					using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
						{
							bw.Write (buffer, 0, len);
						}
					}
				}
			}
		}

		void OnBtnAddClick (object sender, EventArgs e)
		{
			StartActivity (typeof(AddNote));
		}

		void OnBtnViewClick (object sender, EventArgs e)
		{
			StartActivity (typeof(ViewNotes));
		}
	}
}


