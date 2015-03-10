using System;
using System.IO;
using System.Collections.Generic;

namespace NoteApp
{
	public class DatabaseManager 
	{

		static string dbName = "NoteDB.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);

		public DatabaseManager ()
		{
		
		}
			
		public List<Note> ViewAllNotes()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from tblNotes";
					var NoteList = cmd.ExecuteQuery<Note> ();
					return NoteList;
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
				return null;
			}
		}

		public void AddNote(string title,string description)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "insert into tblNotes(Title,Description) values('" + title + "','" + description + "')" ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
			}
		}

		public void EditNote(string title,string description,int noteid)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "update tblNotes set Title='" + title + "', Description='" + description + "' where Noteid=" + noteid ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
			}
		}

		public void DeleteNote(int noteid)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "delete from tblNotes where Noteid = " + noteid ;
					cmd.ExecuteNonQuery();
				}

			} catch(Exception ex) {
				Console.WriteLine ("Error:" + ex.Message);
			}

		}

	}
}

