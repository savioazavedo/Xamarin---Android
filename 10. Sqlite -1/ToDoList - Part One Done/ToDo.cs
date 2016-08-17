using System;
using SQLite;




namespace ToDoList
{
	public class ToDo
	{

		[PrimaryKey, AutoIncrement]
		public int ListID { get; set; }
		public string Title { get; set; }
		public string Details { get; set; }
		public DateTime DateTime { get; set; }


		public ToDo ()
		{
		}
	}
}

