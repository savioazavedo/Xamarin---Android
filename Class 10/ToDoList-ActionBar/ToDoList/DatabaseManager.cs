using System;
using System.IO;
using System.Collections.Generic;

namespace ToDoList
{
	public class DatabaseManager
	{
		static string dbName = "ToDoList.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);
	
		public DatabaseManager ()
		{

		}

		public List<ToDo> ViewAll()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from tblToDoList";
					var NoteList = cmd.ExecuteQuery<ToDo> ();
					return NoteList;
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
				return null;
			}
		}

		public void AddItem(string title,string details)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "insert into tblToDoList(Title,Details) values('" + title + "','" + details + "')" ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
			}
		}

		public void EditItem(string title,string details,int listid)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "update tblToDoList set Title='" + title + "', Details='" + details + "' where Listid=" + listid ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
			}
		}

		public void DeleteItem(int listid)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "delete from tblToDoList where Listid = " + listid ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception ex) {
				Console.WriteLine ("Error:" + ex.Message);
			}
		}
			
	}
}

