using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace heliosweather
{
    public class DatabaseManager
    {
        static string dbName = "weather.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);

        public DatabaseManager(){ }

        //Function for reading locations from the database
        public List<ClassLocations> readLocations()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    //Select word from given category
                    cmd.CommandText = "SELECT * FROM LOCATIONS";
                    var LocationList = cmd.ExecuteQuery<ClassLocations>();
                    return LocationList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
                return null;
            }

        }

        //Function for executing qurey to the database, pass in query string
        public void runQuery(string query)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = query;
                    var rowcount = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }

        }


    }
}