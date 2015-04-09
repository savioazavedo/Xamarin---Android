using System;
using Parse;

namespace Feedr
{
	public class Posts
	{
	
		public string ObjectId { get; set; }
		public string Description { get; set; }
		public string URL { get; set; }
		public ParseFile Image { get; set; }
		public DateTime? createdAt { get; set; }
		public DateTime? updatedAt { get; set; }

		public Posts ()
		{


		}
	}
}

