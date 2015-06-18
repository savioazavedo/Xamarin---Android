using System;
using SQLite;

namespace SingHallelujah
{
	public class Song
	{
	
		[PrimaryKey, AutoIncrement]
		public int SongID { get; set; }
		public string SongName { get; set; }
		public string Lyrics { get; set; }

		public Song ()
		{
		}
	}
}

