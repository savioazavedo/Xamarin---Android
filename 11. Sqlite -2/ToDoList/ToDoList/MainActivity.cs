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
	[Activity (Label = "ToDoList", MainLauncher = true, Icon = "@drawable/icon",Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : Activity
	{

		ListView lstToDoList;
		List<ToDo> myList;

        EditText txtSearchItem;

        static string dbName = "ToDoList.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString(),
                                      Android.OS.Environment.DirectoryDocuments.ToString (), 
                                      dbName);
		DatabaseManager objDb;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			lstToDoList = FindViewById<ListView> (Resource.Id.listView1);

			//Copies the file to your mobile phone
			CopyDatabase ();

			objDb = new DatabaseManager();
			myList = objDb.ViewAll();
			lstToDoList.Adapter = new DataAdapter(this,myList);
			lstToDoList.ItemClick += OnLstToDoListClick;

            txtSearchItem = FindViewById<EditText>(Resource.Id.txtSearch);

            Toast.MakeText(this,"Path=" + dbPath,ToastLength.Long).Show();
            Console.WriteLine("Path=" + dbPath);

            txtSearchItem.TextChanged += TxtSearchItem_TextChanged;


        }

        private void TxtSearchItem_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            String searchQuery = e.Text.ToString();

            myList = objDb.SearchAll(searchQuery);

            if (myList != null)
            {
                lstToDoList.Adapter = new DataAdapter(this, myList);
            }
           
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


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			menu.Add("Add");
			return base.OnPrepareOptionsMenu(menu);
		}

		 void OnLstToDoListClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var ToDoItem = myList[e.Position];
			var edititem = new Intent (this, typeof(EditItem));

			edititem.PutExtra ("Title", ToDoItem.Title);
			edititem.PutExtra ("Details", ToDoItem.Details);
			edititem.PutExtra ("ListID", ToDoItem.ListId);

			StartActivity (edititem);
		}



		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var itemTitle = item.TitleFormatted.ToString();
	
			switch (itemTitle)
			{
				case "Add":
				StartActivity (typeof(AddItem));
				break;
			}
			return base.OnOptionsItemSelected(item);
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			objDb = new DatabaseManager();
			myList = objDb.ViewAll();
			lstToDoList.Adapter = new DataAdapter(this,myList);
		}

	}
}


