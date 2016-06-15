using System;
using System.IO;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ToDoList
{
	[Activity (Label = "ToDoList", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		ListView lstToDoList;
		List<ToDo> myList;
		static string dbName = "ToDoList.sqlite";
		string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
		DatabaseManager objDb;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			lstToDoList = FindViewById<ListView> (Resource.Id.listView1);

			CopyDatabase ();

			objDb = new DatabaseManager ();
			myList = objDb.ViewAll ();
			lstToDoList.Adapter = new DataAdapter (this, myList);

		}
		public void CopyDatabase()
		{
			if (!File.Exists (dbPath))
			
			{
				using (BinaryReader br = new BinaryReader (Assets.Open (dbName)))
				
				{
					using (BinaryWriter bw = new BinaryWriter (new FileStream (dbPath, FileMode.Create)))
					
					{
						byte[] buffer = new byte[2048];
						int len = 0;
						while ((len = br.Read (buffer, 0, buffer.Length)) > 0) 
						
						{
							bw.Write (buffer, 0, len);
						}


					}

				}

			}
		}
	
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			menu.Add ("Add");
			return base.OnPrepareOptionsMenu (menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString ();

			switch (itemTitle)

			{

				case "Add":

				break;
			}
			return base.OnOptionsItemSelected (item);
		}

	
	}

}


