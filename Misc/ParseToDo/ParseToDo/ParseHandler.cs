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

		public List<ToDo> Items { get; private set;}

		private ParseUser userinstance;

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

	}
}

