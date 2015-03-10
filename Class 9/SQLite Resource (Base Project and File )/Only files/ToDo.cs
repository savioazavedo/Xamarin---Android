using System;
using SQLite;

namespace ToDoList
{
		public class ToDo
		{

			[PrimaryKey, AutoIncrement]
			public int ListId { get; set; }
			public string Title{ get; set; }
			public string Details{ get; set; }
			public DateTime Date { get; set;}

			public ToDo ()
			{
			}
		}
}

