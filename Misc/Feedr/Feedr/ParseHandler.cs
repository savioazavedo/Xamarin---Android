using System;
using Parse;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feedr
{
	public class ParseHandler
	{

		static ParseHandler todoServiceInstance = new ParseHandler();
		public static ParseHandler Default { get { return todoServiceInstance; } }
		private ParseHandler () { }
		public List<Posts> Items { get; private set;}

		public async Task CreateUserAsync (string username,string email,string password,byte[] profilepic)
		{
		
			if (username != "" && email != "" && password != "" && profilepic != null)
			{
				ParseFile file = new ParseFile("avatar.jpg", profilepic);
				await file.SaveAsync();

				var user = new ParseUser ()
				{
					Username = username,
					Password = password,
					Email = email
				}; 

				user["ProfilePic"] = file;
						
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

//		public async Task<Boolean> AddPost()
//		{
//
//		}

//			
//		public async Task<Boolean> AddToDoItem(string ItemDescription)
//		{
//			ParseObject ToDo = new ParseObject("ToDo");
//			ToDo["ItemDescription"] = ItemDescription;
//			ToDo ["User"] = ParseUser.CurrentUser;
//
//			try
//			{
//				await ToDo.SaveAsync ();
//				return true;
//			}
//			catch (Exception e) 
//			{
//				Console.WriteLine ("Error:" + e.Message);		
//				return false;
//			}
//		}
			
		public ParseUser GetCurrentUserInstance()
		{
			return ParseUser.CurrentUser;
		}
			
	}
}

