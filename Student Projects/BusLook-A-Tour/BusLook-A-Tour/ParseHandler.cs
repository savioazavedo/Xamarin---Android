using System;
using System.Linq;
using Parse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusLookATour
{
	public class ParseHandler
	{

		static ParseHandler todoServiceInstance = new ParseHandler();
		public static ParseHandler Default { get { return todoServiceInstance;} }
		private ParseHandler () {
		}

		public async Task<List<Busit>> GetAll ()
		{
			var query = ParseObject.GetQuery ("tblServiceUpdates");
			var result = await query.FindAsync ();

			var UpdatesList = new List<Busit> ();

			foreach (var obj in result) {
			
				Busit tempobj = new Busit ();

				tempobj.ObjectId = obj.ObjectId;
				tempobj.Title = Convert.ToString (obj ["Title"]);
				tempobj.createdAt = obj.CreatedAt;
				tempobj.updatedAt = obj.UpdatedAt;
				//tempobj.Description = Convert.ToString (obj ["Description"]);
				tempobj.Details = Convert.ToString (obj ["Details"]);
				//tempobj.DateFrom = Convert.ToString (obj ["DateFrom"]);
				//tempobj.DateTo = Convert.ToString (obj ["DateTo"]);

				UpdatesList.Add (tempobj);
			}
			return UpdatesList;
		}
			
	}
}

