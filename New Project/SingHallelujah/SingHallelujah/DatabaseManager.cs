using System;
using System.IO;
using System.Linq;
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
				Console.WriteLine("Error:" + e.Message);
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
				Console.WriteLine("Error:" + e.Message);
				return null;
			}
		}

		public void AddToFavorites(int Songid,Boolean bFavorite)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "Update tblSongs set Favorite='" + bFavorite + "' where songID=" + Songid; 
					var SongCount = cmd.ExecuteNonQuery();
				}

			} catch(Exception e) {
				Console.WriteLine("Error:" + e.Message);
			}
		}

		public Boolean CheckFavorite(int SongId)
		{
			try
			{
				using (var conn = new SQLite.SQLiteConnection (dbPath)) {
					var cmd = new SQLite.SQLiteCommand (conn);
					cmd.CommandText = "select * from tblSongs where songID=" + SongId; ; 
					var SongList = cmd.ExecuteQuery <Song>();

					if(SongList.Count >=1) 
					{
						//return true;
						if(SongList[0].Favorite == "True")
							return true;
						else 
							return false;
					} else {
						return false;
					}
				}
			} catch(Exception e) {
				Console.WriteLine("Error:" + e.Message);
				return false;
			}
		}


	}
}

