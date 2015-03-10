using System;
using System.IO;
using System.Collections.Generic;

namespace ToDoList
{
	public class DatabaseManager
	{
		static string dbName = "ToDoList.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

		public DatabaseManager ()
		{

		}
		public List<ToDo> ViewAll()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath))
				{
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "Select * from tblToDoList";
					var NoteList = cmd.ExecuteQuery<ToDo> ();
					return NoteList;
				}

			}
			catch(Exception e) 

			{
				Console.WriteLine ("Error: " + e.Message);
				return null;

			}

		}
	}
}

