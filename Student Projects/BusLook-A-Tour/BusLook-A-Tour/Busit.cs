using System;
using Parse;

namespace BusLookATour
{
	public class Busit
	{
		public string ObjectId { get; set; }
		public string Title { get; set; }
		public  DateTime? createdAt { get; set; }
		public  DateTime? updatedAt { get; set; }
		public string Description { get; set; }
		public string Details { get; set; }
		public string DateFrom { get; set; }
		public string DateTo { get; set; }
	}
}

;