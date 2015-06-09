using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RestSharp;
using System;


namespace EventFinda
{
	public class RestHandler
	{
		private string url;

		private IRestResponse ResponseEvents;
		public RestHandler ()
		{
			url = "";
		}
		public RestHandler (string  lurl)
		{
			url = lurl;
		}
		public  Events ExecuteRequest()
		{
			var client = new RestClient (url);
			var request = new RestRequest ();
			ResponseEvents = client.Execute (request);

			XmlSerializer serializer = new XmlSerializer (typeof(Events));
			Events objRss;
			TextReader sr = new  StringReader (ResponseEvents.Content);
			objRss = (Events)serializer.Deserialize (sr);
			return objRss;

		}
		public async Task<Events> ExecuteRequestAsync()
		{
			var client = new RestClient (url);
			var request = new RestRequest ();

			client.Authenticator = new HttpBasicAuthenticator("findyourevents", "vqb7nk57w2km");
			//request.AddParameter ("USERNAME","findyourevents");
			//request.AddParameter ("PASSWORD","vqb7nk57w2km");

			ResponseEvents = await client.ExecuteTaskAsync (request);
			XmlSerializer serializer = new XmlSerializer (typeof(Events));
			Events objRss;


			TextReader sr = new StringReader (ResponseEvents.Content);
			objRss = (Events)serializer.Deserialize (sr);
			return objRss;
		}

	}
}


