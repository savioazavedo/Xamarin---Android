using System;
using System.IO;
using System.Collections.Generic;


namespace SingHallelujah
{
	public class DatabaseManager 
	{
		static string dbName = "SingHalleluiah.sqlite";
		string dbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), dbName);

		public DatabaseManager ()
		{

		}

		public List<Song> ViewAll()
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from tblSongs";
					var SongList = cmd.ExecuteQuery<Song> ();
					return SongList;
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
				return null;
			}
		}


		public List<Song> SearchByName(string songname)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from tblSongs where songname like '%" + songname + "%'"; 
					var SongList = cmd.ExecuteQuery<Song> ();
					return SongList;
				}

			} catch(Exception e) {
				Console.WriteLine ("Error:" + e.Message);
				return null;
			}
		}
	}
}

