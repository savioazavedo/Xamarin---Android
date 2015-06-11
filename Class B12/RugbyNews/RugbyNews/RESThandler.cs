using System;
using RestSharp;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RugbyNews
{
	public class RESThandler
	{

		private string url;
		private IRestResponse response;

		public RESThandler ()
		{
			url = "";
		}

		public RESThandler(string lurl)
		{
			url = lurl;
		}

		public Rss ExecuteRequest()
		{
			var client = new RestClient (url);
			var request = new RestRequest ();

			response = client.Execute(request);

			XmlSerializer serializer = new XmlSerializer(typeof(Rss));
			Rss objRss;

			TextReader sr = new StringReader(response.Content);
			objRss = (Rss) serializer.Deserialize(sr);
			return objRss;
		}

		public async Task<Rss> ExecuteRequestAsync()
		{

			var client = new RestClient (url);
			var request = new RestRequest ();
		
			response  = await client.ExecuteTaskAsync(request);

			XmlSerializer serializer = new XmlSerializer(typeof(Rss));
			Rss objRss;

			TextReader sr = new StringReader(response.Content);
			objRss = (Rss) serializer.Deserialize(sr);
			return objRss;
		}
	}
}

