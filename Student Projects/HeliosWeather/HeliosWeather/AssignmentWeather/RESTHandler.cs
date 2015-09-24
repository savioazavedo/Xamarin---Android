using RestSharp;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace heliosweather
{
    public class RESThandler
    {
        private string url;
        private IRestResponse response;

        public RESThandler()
        {
            url = "";
        }

        public RESThandler(string lurl)
        {
            url = lurl;
        }

        // REST for Current Day Conditions - Async
        public async Task<C_Response> ExecuteRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            XmlSerializer serializer = new XmlSerializer(typeof(C_Response));
            C_Response objRss;

            TextReader sr = new StringReader(response.Content);
            objRss = (C_Response)serializer.Deserialize(sr);
            return objRss;
        }

        // REST for 10 Day Forecast - Async
        public async Task<Response> TenDayExecuteRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            XmlSerializer serializer = new XmlSerializer(typeof(Response));
            Response objRss;

            TextReader sr = new StringReader(response.Content);
            objRss = (Response)serializer.Deserialize(sr);
            return objRss;
        }

        // REST for Sun/Moon Conditions - Async
        public async Task<S_Response> SunMoonRequestAsync()
        {
            var client = new RestClient(url);
            var request = new RestRequest();

            response = await client.ExecuteTaskAsync(request);

            XmlSerializer serializer = new XmlSerializer(typeof(S_Response));
            S_Response objRss;

            TextReader sr = new StringReader(response.Content);
            objRss = (S_Response)serializer.Deserialize(sr);
            return objRss;
        }

    }
}