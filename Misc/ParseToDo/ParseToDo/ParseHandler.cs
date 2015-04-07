using System;
using Parse;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParseToDo
{
	public class ParseHandler
	{

		static ParseHandler todoServiceInstance = new ParseHandler();
		public static ParseHandler Default { get { return todoServiceInstance; } }
		private ParseHandler () { }
		public List<ToDoList> Items { get; private set;}

		public async Task CreateUserAsync (string username,string email,string password)
		{
		
			if (username != "" && email != "" && password != "")
			{
				var user = new ParseUser ()
				{
					Username = username,
					Password = password,
					Email = email
				};
						
				await user.SignUpAsync ();
			}
		}

		public async Task<Boolean> CheckIfUserNameExists (string username)
		{
		
			var query = ParseUser.Query;
			var queryresult = await query.WhereEqualTo ("username", username).FindAsync();

			if (queryresult.ToList().Count > 0) {
				return true;
			} else {
				return false;
			}
		}


		public async Task<Boolean> Login(string username,string password)
		{
			try
			{
				await ParseUser.LogInAsync(username,password);
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine ("Login Failed:" + e.Message);
				return false;
			}
		}
			
		public async Task<Boolean> AddToDoItem(string ItemDescription)
		{
			ParseObject ToDoList = new ParseObject("ToDo");
			ToDoList["ItemDescription"] = ItemDescription;
			ToDoList ["User"] = ParseUser.CurrentUser;

			try
			{
				await ToDoList.SaveAsync ();
				return true;
			}
			catch (Exception e) 
			{
				Console.WriteLine ("Error:" + e.Message);		
				return false;
			}
		}
			
		public ParseUser GetCurrentUserInstance()
		{
			return ParseUser.CurrentUser;
		}
			
	}
}

