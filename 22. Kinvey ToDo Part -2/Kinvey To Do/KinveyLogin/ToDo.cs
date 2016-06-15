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

namespace KinveyLogin
{
    [JsonObject(MemberSerialization.OptIn)]
   public class ToDo
    {
     
            [JsonProperty("_id")]
            public string id { get; set; }
            [JsonProperty]
            public string Title { get; set; }
            [JsonProperty]
            public string Description { get; set; }
            [JsonProperty]
            public User CreatedBy { get; set; }
        
    }
}