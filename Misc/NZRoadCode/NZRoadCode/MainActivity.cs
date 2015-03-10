using System;
using System.IO;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace NZRoadCode
{
	[Activity (Label = "NZRoadCode", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		Button btnBegin;
		static string dbName = "DrivingTest.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Testing an App 
			btnBegin = FindViewById<Button> (Resource.Id.btnBegin);
			btnBegin.Click += OnBeginClick;

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

		public void OnBeginClick(object sender,EventArgs e)
		{
			StartActivity (typeof(TestActivity));
		}

	}
}


