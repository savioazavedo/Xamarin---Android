using System;

namespace EventFinda
{
	public class Helper
	{
		public Helper ()
		{

		}

		public string removecdata(string input) {

			string cdata = "<![CDATA[";
			string cdata2 = "]]>";
			string final = input;


			if (input.Contains (cdata) && input.Contains (cdata2)) {
				string temp = input.Replace (cdata, "");
				final = temp.Replace (cdata2, "");
			}
			return final;

		}

	}
}

