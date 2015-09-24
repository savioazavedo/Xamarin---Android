using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Views.InputMethods;

namespace heliosweather
{   //Set theme and lock screen orientation
    [Activity(Label = "NewLocation", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class NewLocation : Activity
    {
        //List for returned locations
        List<ClassLocations> locationsList;

        //Layout elements
        ListView listViewLocations;
        Button btnGetLocation;
        EditText txtLocation;

        //For SQLite
        string SQLQuery;
        DatabaseManager objDb;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddLocation);

            //Create a new list for loactions
            locationsList = new List<ClassLocations>();

            //Initialise components
            viewSetup();
        }

        private void viewSetup()
        {
            //List plus list click
            listViewLocations = FindViewById<ListView>(Resource.Id.ListFavs_Locations);
            listViewLocations.ItemClick += listViewLocations_ItemClick;

            //Edit text feild plus keyboard click on this field
            txtLocation = FindViewById<EditText>(Resource.Id.editLocation_Locations);
            txtLocation.KeyPress += txtLocation_KeyPress;
            
            //Button plus button click
            btnGetLocation = FindViewById<Button>(Resource.Id.btnAddLocation_Locations);
            btnGetLocation.Click += btnGetLocation_Click;
        }

        void listViewLocations_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {   //Item in listview has been clicked

            if (locationsList[0].town == "No Results") //If item clicked contains no result
            {
                return; //Do nothing
            }
            else //If item contains a return result
            {
                //Write the selected item to the database
                writeDatabaseLocation(e.Position);

                //Send the new location to the main activity
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("Lat", locationsList[e.Position].latitude);
                intent.PutExtra("Long", locationsList[e.Position].longitude);
                //Close the previous open main activity in the stack
                intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                //Start a new main activity
                StartActivity(intent);
                //Destory this activity
                Finish();
            }
        }

        void btnGetLocation_Click(object sender, EventArgs e)
        {   //On button click
           
            //Get Location
            getLocations();
            //Close Keyboard
            closeKeyboard();
        }

        void txtLocation_KeyPress(object sender, View.KeyEventArgs e)
        {   
            //When enter key is pushed while in edittext field
            e.Handled = false;
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            {
                //If return key pressed get location so they dont have to press the screen button
                getLocations();
                //Close the keyboard
                closeKeyboard();
                e.Handled = true;
            }
        }

        private void closeKeyboard()
        {   //Close open keyboard
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(btnGetLocation.WindowToken, 0);
        }

        private async void getLocations()
        {   
            //Get input from editext box
            string location;
            location = txtLocation.Text;
            //Create new Geocoder
            var geo = new Geocoder(this);

            try//Use to catch IO errors from geo.GetFromLocationNameAsync (eg geo service down or no data network)
            {
                //Send location to be Geocoded Async
                var addresses = await geo.GetFromLocationNameAsync(location, 3);

                if (addresses.Count > 0) //If we have returned results
                {   //Clear list
                    locationsList.Clear();
                    //Convert collection to a list
                    addresses.ToList();

                    foreach (var item in addresses) //For each item in the list off returned addresses
                    {
                        //Create a new location object and add current data
                        ClassLocations newLocation = new ClassLocations()
                        {
                            latitude = item.Latitude.ToString(),
                            longitude = item.Longitude.ToString(),
                            country = item.CountryName,
                            town = item.FeatureName
                        };
                        //Add to location list
                        locationsList.Add(newLocation);
                    }
                    //Set location list to listview
                    listViewLocations.Adapter = new DataAdapterAddLoactions(this, locationsList);
                }
                else  //If we have no returned results
                {
                    //Create a new location object and add No Results data
                    ClassLocations newLocation = new ClassLocations()
                    {
                        latitude = " ",
                        longitude = " ",
                        country = " ",
                        town = "No Results"
                    };
                    //Clear list
                    locationsList.Clear();
                    //Add no results to location list
                    locationsList.Add(newLocation);
                    //Set location list to listview
                    listViewLocations.Adapter = new DataAdapterAddLoactions(this, locationsList);
                }
            }
            catch (Exception)//On GeoCoder exception
            {
                //Display error

                //Create a new location object and add No Results data
                ClassLocations newLocation = new ClassLocations()
                {
                    latitude = " ",
                    longitude = " ",
                    country = "No Connection",
                    town = "No Results"
                };
                //Clear list
                locationsList.Clear();
                //Add no results to location list
                locationsList.Add(newLocation);
                //Set location list to listview
                listViewLocations.Adapter = new DataAdapterAddLoactions(this, locationsList);
                
            }

        }

        private void writeDatabaseLocation(int xPosition)
        {   //Send SQLite query to write to database

            //Construct query to insert new entry into database
            //Entry values from returned GeoCoder location in position of which item clicked in the list
            SQLQuery = "INSERT INTO locations (country, town, latitude, longitude) VALUES ('" +
                locationsList[xPosition].country + "','" + locationsList[xPosition].town + "','" +
                locationsList[xPosition].latitude + "','" + locationsList[xPosition].longitude + "')";
            //Send query to database manager
            objDb = new DatabaseManager();
            objDb.runQuery(SQLQuery);
        }
    }
}