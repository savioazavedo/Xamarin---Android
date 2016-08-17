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
using SQLite;

namespace BookDatabase
{
    class Grocery
    {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            [Indexed]
            public string Item { get; set; }
            public DateTime Time { get; set; }
            public decimal Price { get; set; }

            public Boolean Completed { get; set; }
      
    }
}