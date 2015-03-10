using System;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.OS;
using RestSharp;
using System.Text;
using System.Xml.Serialization;

namespace HTTPClient
{
	[Activity (Label = "HTTPClient", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			ScrollView scview = FindViewById<ScrollView> (Resource.Id.scrollView1);
			TextView text = FindViewById<TextView> (Resource.Id.textView1);

			button.Click += delegate {
			
//				var request = HttpWebRequest.Create(@"http://api.openweathermap.org/data/2.5/weather?q=London&mode=xml");
//				request.ContentType = "text/xml";
//				request.Method = "GET";
//				using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
//				{
//					if (response.StatusCode != HttpStatusCode.OK)
//						Console.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
//					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
//					{
//						var content = reader.ReadToEnd();
//						//text.Text = Convert.ToString (content);
//						 
//						XmlSerializer serializer = new XmlSerializer(typeof(Current));
//						Current objCurrent;
//						objCurrent = (Current) serializer.Deserialize(reader);
//
//						text.Text = text.Text + " " + objCurrent.Humidity.Value;
//						text.Text = text.Text + " " + objCurrent.Temperature.Value;
//
//					}
//				}

				var client = new RestClient (@"http://api.openweathermap.org/data/2.5/weather?q=London&mode=xml");

				var request = new RestRequest ();
				var response = client.Execute(request);
				//text.Text = response.Content;

				XmlSerializer serializer = new XmlSerializer(typeof(Current));
				Current objCurrent;

				TextReader sr = new StringReader(response.Content);
				objCurrent = (Current) serializer.Deserialize(sr);

				text.Text = text.Text + " " + objCurrent.Humidity.Value;
				text.Text = text.Text + " " + objCurrent.Temperature.Value;

			};

		}
			

	}
}


