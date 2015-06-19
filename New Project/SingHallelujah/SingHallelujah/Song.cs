using System;
using SQLite;

namespace SingHallelujah
{
	public class Song
	{
	
		[PrimaryKey, AutoIncrement]
		public int SongId { get; set; }
		public string SongName { get; set; }
		public string Lyrics { get; set; }
		public string Favorite { get; set;}

		public Song ()
		{
		}
	}
}

