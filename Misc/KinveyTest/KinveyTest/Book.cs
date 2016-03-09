using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using KinveyXamarin;

namespace KinveyTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Book
    {
        [JsonProperty("_id")]
        public string id { get; set; }
        [JsonProperty]
        public string BookName { get; set; }
        [JsonProperty]
        public string Author { get; set; }
        [JsonProperty]
        public string Genre { get; set; }       
    }
}