using System;
using System.IO;
using System.Collections.Generic;

namespace NZRoadCode
{
	public class DatabaseManager
	{

		static string dbName = "DrivingTest.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);

		public DatabaseManager ()
		{
		}

		public List<DrivingTest> GetQuestion()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from NZ_Driving_Test";
					var Question = cmd.ExecuteQuery<DrivingTest> ();
					return Question;
				}
			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
				return null;
			}
		}

	}
}

