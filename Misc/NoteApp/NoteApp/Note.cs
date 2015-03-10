using System;
using SQLite;

namespace NoteApp
{
	public class Note
	{
		[PrimaryKey, AutoIncrement]
		public int NoteID { get; set; }
		public string Title{ get; set; }
		public string Description{ get; set; }

		public Note ()
		{

		}
			
	}
}

