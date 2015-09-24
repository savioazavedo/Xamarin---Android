using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Net;
using System.Timers;

namespace heliosweather
{   //Set theme and lock screen orientation
    [Activity(Label = "HeliosWeather", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashActivity : Activity , ILocationListener
    {
        //Set location manager for GPS
        LocationManager locMgr;
        string xLat, xLong;

        //Timer if no position update given
        private static Timer locationTimer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Set splash layout
            SetContentView(Resource.Layout.Splash);

            // Create a timer with a 10sec interval.
            locationTimer = new Timer(10000);

            // Hook up the Elapsed event for the timer. 
            locationTimer.Elapsed += OnTimedEvent;
            
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            //No location response within 10sec

            //Stop timer
            locationTimer.Enabled = false;

            //Stop any more updates that could still be coming
            locMgr.RemoveUpdates(this);

            //Start main activity and send through that we have no provider
            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("Lat", "NoProvider");
            StartActivity(intent);
            //Destory this activity
            Finish();
        }

        protected override void OnResume()
        {   //on app resume
            base.OnResume();

            // initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;
            //Get current location
            GetLocation();
        }

        void GetLocation()
        {   
            //Setup criteria
            Criteria locationCriteria = new Criteria();

            //Set requirements for criteria
            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;

            //Get the best provider matching set criteria
            string locationProvider = locMgr.GetBestProvider(locationCriteria, true);

            bool result = locMgr.IsProviderEnabled(locationProvider);

            if (locationProvider != null) //If we get a provider
            {
                //Request a GPS updates to start
                locMgr.RequestLocationUpdates(locationProvider, 0, 1, this);
                //Start timer. If no location within 10sec, use one in database
                locationTimer.Enabled = true;
            }
            else  //If we don't get a provider
            {
                //Set Lat to "NoProvider" and send through to main activity, test for it there
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("Lat", "NoProvider");
                StartActivity(intent);
                //Destory this activity
                Finish();
            }
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {   //When we get a loction update

            //Stop timer as we have got a response
            locationTimer.Enabled = false;

            //Catch latitude and longitude
            xLat = location.Latitude.ToString();
            xLong = location.Longitude.ToString();
           
            //Stop any more updates
            locMgr.RemoveUpdates(this);
            
            //Start main activity and send through Latitude and Longitude
            var intent = new Intent(this, typeof(MainActivity));
            intent.PutExtra("Lat", xLat);
            intent.PutExtra("Long", xLong);
            StartActivity(intent);
            //Destory this activity
            Finish();
        }

        protected override void OnDestroy()
        {
            //Make sure timer is set off on destroy
            base.OnDestroy();
            locationTimer.Enabled = false;
        }

        //Required for ILocationListener
        public void OnProviderEnabled(string provider) {  }
        public void OnProviderDisabled(string provider) {  }
        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

    }
}

