using System;
using System.Text.RegularExpressions;

namespace SingHallelujah
{

	public class Helper
	{
		private static Helper instance;

		private Helper() {}

		public static Helper Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new Helper();
				}
				return instance;
			}
		}

		public string ProcessString (string input)
		{
			string replacement = Regex.Replace(input, @"\t|\n|\r", "");
			replacement = replacement.Replace ("\"", "").Substring (0, 20) + " ...";
			replacement = Regex.Replace(replacement, @"  |   ", " ");;
			return replacement;
		}

		public string RemoveQuote(string input)
		{
			string replacement = input.Replace ("\"", "") ;
			return replacement;
		}

		public string RemoveTabsandSpaces(string input)
		{
			string replacement = Regex.Replace(input, @"\t|\n|\r", "");
			replacement = Regex.Replace(replacement, @"  |   ", " ");;
			return replacement;
		}

	}
}

