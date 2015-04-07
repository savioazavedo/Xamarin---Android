using System;
using Parse;

namespace ParseToDo
{
	public class ToDoList
	{
	
			public int objectId { get; set; }
		    public string ItemDescription{ get; set; }
			public ParseUser User { get; set;}
			public DateTime createdAt { get; set; }
			public DateTime updatedAt { get; set;}

			public ToDoList ()
			{
				
			}
			
	}
}

