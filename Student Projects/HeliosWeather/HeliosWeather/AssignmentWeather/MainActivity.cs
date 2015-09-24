using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using System.IO;
using Android.Locations;
using AndroidHUD;
using System.Timers;

namespace heliosweather
{   //Set theme and lock screen orientation
    [Activity(Label = "MainActivity", Theme = "@style/WeatherTheme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, ILocationListener
    {
        //Toolbar and Left Drawer components
        private SupportToolbar mToolBar; 
        ListView left_drawer;
        DrawerLayout drawer_layout;
        Button addLocation;

        //Text views Current Day
        TextView txtLocation_CurrentDay, txtDateTime_CurrentDay, txtTemperature_CurrentDay, txtCondition_CurrentDay,
                 txtFeelsLike_CurrentDay, txtHigh_CurrentDay, txtLow_CurrentDay;
        
        //Text views Current Day Details
        TextView txtWind_Details, txtHumidity_Details,txtVisibility_Details, txtPrecipitation_Details;

        //10Day Forecast
        TextView[] tendayforecast;
        ImageView[] imgIcon10Day;

        //Image resources
        ImageView imgBackgroundIcon, ImgIcon_CurrentDay;
        string[] iconNames;
        int[] backgrounds, whiteIconsL, largeIcons, medIcons, nightIcons, textView10Days, imgView10Days;

        //Layouts
        FrameLayout FLBackground;
        LinearLayout LLDayNightToggle;

        //Responses
        C_Response ConditionResponse;
        Response ForecastResponse;
        S_Response SunMoonResponse;

        //Location data
        List<ClassLocations> locationsList;
        string currentLat, currentLong;
       
        //Resthandler
        RESThandler objRest;
        
        //SQLite
        static string dbName = "weather.sqlite";
        string dbPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.ToString(), dbName);
        DatabaseManager objDb;
        string SQLQuery;

        //GPS
        LocationManager locMgr;

        //Timer if no position update given
        private static Timer locationTimer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //Set toolbar
            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
            SetSupportActionBar(mToolBar);

            // Create a timer with a 10sec interval.
            locationTimer = new Timer(10000);

            // Hook up the Elapsed event for the timer. 
            locationTimer.Elapsed += OnTimedEvent;

            //If not there, copy database to device before any database operations
            CopyDatabase();

            //Setup components
            viewSetup();

            //Left Nav draw and click handler setup
            left_drawer = FindViewById<ListView>(Resource.Id.left_drawer);
            drawer_layout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            left_drawer.ItemClick += left_drawer_ItemClick;

            //Get Latitude and Longitude passed from 'splash screen' GPS read or 'new location' activity
            currentLat = Intent.GetStringExtra("Lat");
            currentLong = Intent.GetStringExtra("Long");

            //If Lat was passed as "NoProvider", use the last current location, read from database
            if (currentLat == "NoProvider")
            {   //read latitude and longitude from data base
                readDatabaseLocations();
                //current location is always the first on in the table
                currentLat = locationsList[0].latitude;
                currentLong = locationsList[0].longitude;
            }
            else  //Otherwise write current location to database and then read back for display
            {
                //Write into database new current location
                writeDatabaseLocation();
                //Read all locations for populating menu
                readDatabaseLocations();
            }
            
            //Set location list in Left Nav drawer
            SetLocationButtons();

            //Get weather data from API
            getAPIData();
        }

        void left_drawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {   //When item in left nav draw is clicked
            
            //If it is for the current location
            if (locationsList[e.Position].town == "Current Location")
            {   //Get the current location
                GetLocation();
            }
            else //Otherwise display the selected location
            {
                //set latitude and longitude using the position of selected item in the list
                currentLat = locationsList[e.Position].latitude;
                currentLong = locationsList[e.Position].longitude;
                //get weather data for selected location
                getAPIData();
            }
            //We are finished, close the drawer
            drawer_layout.CloseDrawers();
        }

        private void SetLocationButtons()
        {   //populate the Left Nav draw list with locations
            left_drawer.Adapter = new DataAdapterLoactions(this, locationsList);
        }

        private async void getAPIData()
        {   //Show wait GUI while getting weather from API
            AndHUD.Shared.Show(this, null, -1, MaskType.Black, null, null, true, null);

            try  //Catch for internet or IO errors
            {
                //Query API for current conditions
                objRest = new RESThandler(@"http://api.wunderground.com/api/8e858711922efaf2/conditions/q/" + currentLat + "," + currentLong + ".xml");
                ConditionResponse = await objRest.ExecuteRequestAsync();
                //Query API for 10 day forecast
                objRest = new RESThandler(@"http://api.wunderground.com/api/8e858711922efaf2/forecast10day/q/" + currentLat + "," + currentLong + ".xml");
                ForecastResponse = await objRest.TenDayExecuteRequestAsync();
                //Query API for sun/moon data
                objRest = new RESThandler(@"http://api.wunderground.com/api/8e858711922efaf2/astronomy/q/" + currentLat + "," + currentLong + ".xml");
                SunMoonResponse = await objRest.SunMoonRequestAsync();

                //Once returned, display weather data
                displayWeatherData();
            }
            catch (Exception)
            {
                //Get weather failed, turn of wait UI
                AndHUD.Shared.Dismiss(this);

                //Display error message
                AndHUD.Shared.ShowError(this, "Cannot get weather data! \nMake sure you have \nan internet connection", MaskType.Black, TimeSpan.FromSeconds(10), null, null);
            }
        }

        private void displayWeatherData()
        {
            //Get offset for date time from API and convert to INT (eg +1200 converts to 1200)
            int offset = Convert.ToInt32(ConditionResponse.Current_observation.Local_tz_offset);
            //Convert API Epoch time in seconds to Local date time. Use offset * 36 to convert hours into seconds and add to Epoch
            DateTime tempdate = epoch2string(Convert.ToDouble(ConditionResponse.Current_observation.Local_epoch) + (offset * 36));

            //Round the returned temperature (because the API sometimes gives, eg 15.5 or 15.56!!)
            //So I am not displaying decimal places
            double tempC = Math.Round(Convert.ToDouble(ConditionResponse.Current_observation.Temp_c));
            double feelsLikeTemp = Math.Round(Convert.ToDouble(ConditionResponse.Current_observation.Feelslike_c));

            //setup to hold icon names
            string iconCondition, iconForecast;
            int iconIndex;
            //get the current icon name
            iconCondition = ConditionResponse.Current_observation.Icon;
            //get the index of this name from my list of icon names
            iconIndex = Array.IndexOf(iconNames, iconCondition);
            
            //Display Current Day data
            txtLocation_CurrentDay.Text = ConditionResponse.Current_observation.Display_location.City;
            txtDateTime_CurrentDay.Text = tempdate.ToString("ddd d" + "   -   " + "h:mm tt"); //Format date
            txtTemperature_CurrentDay.Text = tempC.ToString() + "°";
            txtCondition_CurrentDay.Text = ConditionResponse.Current_observation.Weather;
            txtFeelsLike_CurrentDay.Text = "Feels Like: " + feelsLikeTemp.ToString() + "°";
            txtHigh_CurrentDay.Text = "High: " + ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[0].High.Celsius + "°";
            txtLow_CurrentDay.Text = "Low: " + ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[0].Low.Celsius + "°";

            //Display Current Day Details data
            txtWind_Details.Text = "Wind: " + ConditionResponse.Current_observation.Wind_kph + " Kph";
            txtHumidity_Details.Text = "Humidity: " + ConditionResponse.Current_observation.Relative_humidity;
            txtVisibility_Details.Text = "Visibility: " + ConditionResponse.Current_observation.Visibility_km + " km";
            txtPrecipitation_Details.Text = "Rain Chance: " + ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[0].Pop + "%";

            //Check for night or day
            //24hour time has been returned - add the minutes to the hours eg 1300 (hours) + 45 (mins) = 1345
            //do for sunrise and sunset
            int sunrise = Convert.ToInt32(SunMoonResponse.Sun_phase.Sunrise.Hour + SunMoonResponse.Sun_phase.Sunrise.Minute);
            int sunset = Convert.ToInt32(SunMoonResponse.Sun_phase.Sunset.Hour + SunMoonResponse.Sun_phase.Sunset.Minute);
            //and current time
            int currenttime = Convert.ToInt32(SunMoonResponse.Moon_phase.Current_time.Hour + SunMoonResponse.Moon_phase.Current_time.Minute);


            if(currenttime  > sunrise && currenttime < sunset)  //Daytime
            {   //If current time is within sunrise and sunset - it's daylight

                //Set background icon to current day weather icon large
                imgBackgroundIcon.SetImageResource(largeIcons[iconIndex]);
                //Turn off dark background
                LLDayNightToggle.Alpha = 0;
            }
            else   //Nighttime
            {   //If current time is outside sunrise and sunset - it's dark
                //Set background icon to current night weather icon large
                imgBackgroundIcon.SetImageResource(nightIcons[iconIndex]);
                //Turn on dark background filter for night
                LLDayNightToggle.Alpha = 0.7f;
            }
            
            //Set FrameLayout Background to current weather image
            FLBackground.SetBackgroundResource(backgrounds[iconIndex]);

            //Set current day white icon large to current weather image
            ImgIcon_CurrentDay.SetImageResource(whiteIconsL[iconIndex]);

            //Display 10 day Forecast
            for (int i = 0; i < 9; i++)
            {   //For each day:

                //Convert date
                tempdate = epoch2string(Convert.ToDouble(ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i+1].Date.Epoch) + (offset * 36));

                //Display data, manipulate list indexes to get correct day
                tendayforecast[i * 6].Text = ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i+1].Date.Weekday;
                tendayforecast[(i * 6) + 1].Text = ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i + 1].High.Celsius + "°";
                tendayforecast[(i * 6) + 2].Text = ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i + 1].Low.Celsius + "°";
                tendayforecast[(i * 6) + 3].Text = ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i + 1].Conditions;
                tendayforecast[(i * 6) + 4].Text = tempdate.ToString("dd MMMM yyyy");  //Format date
                tendayforecast[(i * 6) + 5].Text = "Chance of Rain: " + ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i + 1].Pop + "%";
                
                //Display correct icon, match retunred icon description to index in list, use index to get correct drawable
                iconForecast = ForecastResponse.Forecast.Simpleforecast.Forecastdays.Forecastday[i+1].Icon;
                iconIndex = Array.IndexOf(iconNames, iconForecast);
                imgIcon10Day[i].SetImageResource(medIcons[iconIndex]);
                
            }
            //Have got the weather and displayed the screen, turn off wait GUI
            AndHUD.Shared.Dismiss(this);
        }

        private DateTime epoch2string(double epoch)
        {   //Convert Epoch to date format
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch);
        }

        private void viewSetup()
        {
            //Set list of API icon names for matching up with image resources
            iconNames = new string[] {"chanceflurries", "chancerain", "chancerain", "chancesleet", "chancesleet",
            "chancesnow", "chancetstorms", "chancetstorms", "clear", "cloudy", "flurries", "fog",	
            "hazy", "mostlycloudy", "mostlysunny", "partlycloudy", "partlysunny", "sleet", "rain",
            "sleet", "snow", "sunny", "tstorms", "tstorms", "unknown", "cloudy", "partlycloudy"};

            //Get resource id from each image of White Icons Large
            whiteIconsL = new int[] { Resource.Drawable.I_ChanceOfFlurries_64, Resource.Drawable.I_ChanceOfRain_64, Resource.Drawable.I_ChanceRain_64,
            Resource.Drawable.I_ChanceOfFreezingRain_64, Resource.Drawable.I_ChanceOfSleet_64, Resource.Drawable.I_ChanceOfSnow_64, Resource.Drawable.I_ChanceOfThunderstorms_64,
            Resource.Drawable.I_ChanceOfAThunderstorm_64, Resource.Drawable.I_Clear_64, Resource.Drawable.I_Cloudy_64, Resource.Drawable.I_Flurries_64,
            Resource.Drawable.I_Fog_64, Resource.Drawable.I_Haze_64, Resource.Drawable.I_MostlyCloudy_64, Resource.Drawable.I_MostlySunny_64,
            Resource.Drawable.I_PartlyCloudy_64, Resource.Drawable.I_PartlySunny_64, Resource.Drawable.I_FreezingRain_64, Resource.Drawable.I_Rain_64,
            Resource.Drawable.I_Sleet_64, Resource.Drawable.I_Snow_64, Resource.Drawable.I_Sunny_64, Resource.Drawable.I_Thunderstorms_64,
            Resource.Drawable.I_Thunderstorm_64, Resource.Drawable.I_Unknown_64, Resource.Drawable.I_Overcast_64, Resource.Drawable.I_ScatteredClouds_64};

            //Get resource id from each image of Medium Icons
            medIcons = new int[] { Resource.Drawable.M_ChanceOfFlurries, Resource.Drawable.M_ChanceOfRain, Resource.Drawable.M_ChanceRain,
            Resource.Drawable.M_ChanceOfFreezingRain, Resource.Drawable.M_ChanceOfSleet, Resource.Drawable.M_ChanceOfSnow, Resource.Drawable.M_ChanceOfThunderstorms,
            Resource.Drawable.M_ChanceOfThunderstorm, Resource.Drawable.M_Clear, Resource.Drawable.M_Cloudy, Resource.Drawable.M_Flurries,
            Resource.Drawable.M_Fog, Resource.Drawable.M_Haze, Resource.Drawable.M_MostlyCloudy, Resource.Drawable.M_MostlySunny,
            Resource.Drawable.M_PartlyCloudy, Resource.Drawable.M_PartlySunny, Resource.Drawable.M_FreezingRain, Resource.Drawable.M_Rain,
            Resource.Drawable.M_Sleet, Resource.Drawable.M_Snow, Resource.Drawable.M_Sunny, Resource.Drawable.M_Thunderstorms,
            Resource.Drawable.M_Thunderstorm, Resource.Drawable.M_Unknown, Resource.Drawable.M_Overcast, Resource.Drawable.M_ScatteredClouds};
           
            //Get resource id from each image of Large Backgorund Icons
            largeIcons = new int[] { Resource.Drawable.L_ChanceOfFlurries, Resource.Drawable.L_ChanceOfRain, Resource.Drawable.L_ChanceRain,
            Resource.Drawable.L_ChanceOfFreezingRain, Resource.Drawable.L_ChanceOfSleet, Resource.Drawable.L_ChanceOfSnow, Resource.Drawable.L_ChanceOfThunderstorms,
            Resource.Drawable.L_ChanceOfThunderstorm, Resource.Drawable.L_Clear, Resource.Drawable.L_Cloudy, Resource.Drawable.L_Flurries,
            Resource.Drawable.L_Fog, Resource.Drawable.L_Haze, Resource.Drawable.L_MostlyCloudy, Resource.Drawable.L_MostlySunny,
            Resource.Drawable.L_PartlyCloudy, Resource.Drawable.L_PartlySunny, Resource.Drawable.L_FreezingRain, Resource.Drawable.L_Rain,
            Resource.Drawable.L_Sleet, Resource.Drawable.L_Snow, Resource.Drawable.L_Sunny, Resource.Drawable.L_Thunderstorms,
            Resource.Drawable.L_Thunderstorm, Resource.Drawable.L_Unknown, Resource.Drawable.L_Overcast, Resource.Drawable.L_ScatteredClouds};

            //Get resource id from each image of Night Icons
            nightIcons = new int[] { Resource.Drawable.N_L_ChanceOfFlurries, Resource.Drawable.N_L_ChanceOfRain, Resource.Drawable.N_L_ChanceRain,
            Resource.Drawable.N_L_ChanceOfFreezingRain, Resource.Drawable.N_L_ChanceOfSleet, Resource.Drawable.N_L_ChanceOfSnow, Resource.Drawable.N_L_ChanceOfThunderstorms,
            Resource.Drawable.N_L_ChanceOfThunderstorm, Resource.Drawable.N_L_Clear, Resource.Drawable.N_L_Cloudy, Resource.Drawable.N_L_Flurries,
            Resource.Drawable.N_L_Fog, Resource.Drawable.N_L_Haze, Resource.Drawable.N_L_MostlyCloudy, Resource.Drawable.N_L_MostlySunny,
            Resource.Drawable.N_L_PartlyCloudy, Resource.Drawable.N_L_PartlySunny, Resource.Drawable.N_L_FreezingRain, Resource.Drawable.N_L_Rain,
            Resource.Drawable.N_L_Sleet, Resource.Drawable.N_L_Snow, Resource.Drawable.N_L_Sunny, Resource.Drawable.N_L_Thunderstorms,
            Resource.Drawable.N_L_Thunderstorm, Resource.Drawable.N_L_Unknown, Resource.Drawable.N_L_Overcast, Resource.Drawable.N_L_ScatteredClouds};

            //Get resource id from each image of Background images
            backgrounds = new int[] { Resource.Drawable.BG_ChanceOfFlurries, Resource.Drawable.BG_ChanceOfRain, Resource.Drawable.BG_ChanceRain,
            Resource.Drawable.BG_ChanceOfFreezingRain, Resource.Drawable.BG_ChanceOfSleet, Resource.Drawable.BG_ChanceOfSnow, Resource.Drawable.BG_ChanceOfThunderstorms,
            Resource.Drawable.BG_ChanceOfThunderstorm, Resource.Drawable.BG_Clear, Resource.Drawable.BG_Cloudy, Resource.Drawable.BG_Flurries,
            Resource.Drawable.BG_Fog, Resource.Drawable.BG_Haze, Resource.Drawable.BG_MostlyCloudy, Resource.Drawable.BG_MostlySunny,
            Resource.Drawable.BG_PartlyCloudy, Resource.Drawable.BG_PartlySunny, Resource.Drawable.BG_FreezingRain, Resource.Drawable.BG_Rain,
            Resource.Drawable.BG_Sleet, Resource.Drawable.BG_Snow, Resource.Drawable.BG_Sunny, Resource.Drawable.BG_Thunderstorms,
            Resource.Drawable.BG_Thunderstorm, Resource.Drawable.BG_Unknown, Resource.Drawable.BG_Overcast,Resource.Drawable.BG_ScatteredClouds};

            //Get resource id for 10 day forecast text views
            textView10Days = new int[] {Resource.Id.txtDayName_Day1, Resource.Id.txtTempHigh_Day1, Resource.Id.txtTempLow_Day1, Resource.Id.txtConditions_Day1, Resource.Id.txtDate_Day1, Resource.Id.txtChanceRain_Day1,
            Resource.Id.txtDayName_Day2, Resource.Id.txtTempHigh_Day2, Resource.Id.txtTempLow_Day2, Resource.Id.txtConditions_Day2, Resource.Id.txtDate_Day2, Resource.Id.txtChanceRain_Day2,
            Resource.Id.txtDayName_Day3, Resource.Id.txtTempHigh_Day3, Resource.Id.txtTempLow_Day3, Resource.Id.txtConditions_Day3, Resource.Id.txtDate_Day3, Resource.Id.txtChanceRain_Day3,
            Resource.Id.txtDayName_Day4, Resource.Id.txtTempHigh_Day4, Resource.Id.txtTempLow_Day4, Resource.Id.txtConditions_Day4, Resource.Id.txtDate_Day4, Resource.Id.txtChanceRain_Day4,
            Resource.Id.txtDayName_Day5, Resource.Id.txtTempHigh_Day5, Resource.Id.txtTempLow_Day5, Resource.Id.txtConditions_Day5, Resource.Id.txtDate_Day5, Resource.Id.txtChanceRain_Day5,
            Resource.Id.txtDayName_Day6, Resource.Id.txtTempHigh_Day6, Resource.Id.txtTempLow_Day6, Resource.Id.txtConditions_Day6, Resource.Id.txtDate_Day6, Resource.Id.txtChanceRain_Day6,
            Resource.Id.txtDayName_Day7, Resource.Id.txtTempHigh_Day7, Resource.Id.txtTempLow_Day7, Resource.Id.txtConditions_Day7, Resource.Id.txtDate_Day7, Resource.Id.txtChanceRain_Day7,
            Resource.Id.txtDayName_Day8, Resource.Id.txtTempHigh_Day8, Resource.Id.txtTempLow_Day8, Resource.Id.txtConditions_Day8, Resource.Id.txtDate_Day8, Resource.Id.txtChanceRain_Day8,
            Resource.Id.txtDayName_Day9, Resource.Id.txtTempHigh_Day9, Resource.Id.txtTempLow_Day9, Resource.Id.txtConditions_Day9, Resource.Id.txtDate_Day9, Resource.Id.txtChanceRain_Day9};
           
            //Get resource id for 10 day forecast image views
            imgView10Days = new int[] { Resource.Id.ImgIcon_Day1, Resource.Id.ImgIcon_Day2, Resource.Id.ImgIcon_Day3, Resource.Id.ImgIcon_Day4, Resource.Id.ImgIcon_Day5,
            Resource.Id.ImgIcon_Day6, Resource.Id.ImgIcon_Day7, Resource.Id.ImgIcon_Day8, Resource.Id.ImgIcon_Day9};

            //Current Day components
            txtLocation_CurrentDay = FindViewById<TextView>(Resource.Id.txtLocation_CurrentDay);
            txtDateTime_CurrentDay = FindViewById<TextView>(Resource.Id.txtDateTime_CurrentDay);
            txtTemperature_CurrentDay = FindViewById<TextView>(Resource.Id.txtTemperature_CurrentDay);
            txtCondition_CurrentDay = FindViewById<TextView>(Resource.Id.txtCondition_CurrentDay);
            txtFeelsLike_CurrentDay = FindViewById<TextView>(Resource.Id.txtFeelsLike_CurrentDay);
            txtHigh_CurrentDay = FindViewById<TextView>(Resource.Id.txtHigh_CurrentDay);
            txtLow_CurrentDay = FindViewById<TextView>(Resource.Id.txtLow_CurrentDay);

            //Current Day Details components
            txtWind_Details = FindViewById<TextView>(Resource.Id.txtWind_Details);
            txtHumidity_Details = FindViewById<TextView>(Resource.Id.txtHumidity_Details);
            txtVisibility_Details = FindViewById<TextView>(Resource.Id.txtVisibility_Details);
            txtPrecipitation_Details = FindViewById<TextView>(Resource.Id.txtPrecipitation_Details);

            //Weather icons
            imgBackgroundIcon = FindViewById<ImageView>(Resource.Id.imgBackgroundIcon);
            ImgIcon_CurrentDay = FindViewById<ImageView>(Resource.Id.ImgIcon_CurrentDay);

            //Nav Drawer button
            addLocation = FindViewById<Button>(Resource.Id.btnAdd_LeftDrawer);
            addLocation.Click += addLocation_Click;

            //Weather Background
            FLBackground = FindViewById<FrameLayout>(Resource.Id.FLBackground);

            //Night/Day toggle layout
            LLDayNightToggle = FindViewById<LinearLayout>(Resource.Id.LLDayNightToggle);

            //Create a array of imageviews for the 10 day forecast Icons
            imgIcon10Day = new ImageView[9];

            for (int i = 0; i < imgIcon10Day.Length; i++)
            {   //find them all
                imgIcon10Day[i] = FindViewById<ImageView>(imgView10Days[i]);
            }

            //Create forecast text views
            tendayforecast = new TextView[54];

            for (int i = 0; i < tendayforecast.Length; i++)
            {   //find them all
                tendayforecast[i] = FindViewById<TextView>(textView10Days[i]);
            }
        }

        void addLocation_Click(object sender, EventArgs e)
        {
            //Close Nav Drawer
            drawer_layout.CloseDrawers();
            //Open 'add location' Activity
            var intent = new Intent(this, typeof(NewLocation));
            StartActivity(intent);
        }

        private void readDatabaseLocations()
        {   //Create list and fill from database
            objDb = new DatabaseManager();
            locationsList = new List<ClassLocations>();

            locationsList = objDb.readLocations();
        }

        private void writeDatabaseLocation()
        {   //Udate the current location from GPS services to database for future use (eg on no GPS fix)
            SQLQuery = "UPDATE locations SET country = 'Current Location', town = 'Current Location', latitude = '" + 
                currentLat + "', longitude = '" + currentLong + "' WHERE  rowid = 1";
            objDb = new DatabaseManager();
            objDb.runQuery(SQLQuery);
        }

        public void CopyDatabase()
        {
            //If not exists, copy database to device
            if (!File.Exists(dbPath))
            {
                using (BinaryReader br = new BinaryReader(Assets.Open(dbName)))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            // initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;
        }

        public void OnLocationChanged(Location location)
        {   //Location has been returned

            //Stop timer as we have got a response
            locationTimer.Enabled = false;

            currentLat = location.Latitude.ToString();
            currentLong = location.Longitude.ToString();
            //Stop updates
            locMgr.RemoveUpdates(this);

            //Write into database new current location
            writeDatabaseLocation();

            //Have got the location, turn off wait GUI
            AndHUD.Shared.Dismiss(this);
            //Get weather data for current location
            getAPIData();
        }

        void GetLocation()
        {   //Show wait GUI while getting location
            AndHUD.Shared.Show(this, null, -1, MaskType.Black, null, null, true, null);

            //Create and set criteria for provider
            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;

            //get best provider matching criteria
            string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

            if (locationProvider != null) //If we get a provider
            {
                //Request a GPS updates to start
                locMgr.RequestLocationUpdates(locationProvider, 0, 1, this);
                //Start timer. If no location within 10sec, use one in database
                locationTimer.Enabled = true;
            }
            else //If we don't get a provider
            {   //get last location from database instead
                readDatabaseLocations();
                //Set location data
                currentLat = locationsList[0].latitude;
                currentLong = locationsList[0].longitude;
                //Have got the location, turn off wait GUI
                AndHUD.Shared.Dismiss(this);
                //Get the weather for location
                getAPIData();
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            //No location response within 10sec

            //Stop timer
            locationTimer.Enabled = false;

            //Stop any more updates that could still be coming
            locMgr.RemoveUpdates(this);

            //If we don't get a provider
            //get last location from database instead
            readDatabaseLocations();
            //Set location data
            currentLat = locationsList[0].latitude;
            currentLong = locationsList[0].longitude;
            //Have got the location, turn off wait GUI
            AndHUD.Shared.Dismiss(this);
            //Get the weather for location
            getAPIData();
        }

        protected override void OnDestroy()
        {
            //Make sure timer is set off on destroy
            base.OnDestroy();
            locationTimer.Enabled = false;
        }

        public void OnProviderDisabled(string provider){}
        public void OnProviderEnabled(string provider){}
        public void OnStatusChanged(string provider, Availability status, Bundle extras){}
    }
}