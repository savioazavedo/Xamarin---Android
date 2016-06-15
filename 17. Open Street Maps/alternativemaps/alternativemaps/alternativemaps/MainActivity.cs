

using System;
using Android.App;
using Android.Widget;
using Android.OS;


using Osmdroid.TileProvider.TileSource;
using Osmdroid.Util;
using Osmdroid.Views;
using Osmdroid.Api;
using Osmdroid.Views.Overlay;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Osmdroid;



namespace alternativemaps
{
    [Activity(Label = "OSMMAPS", MainLauncher = true, Icon = "@drawable/icon")]
    


class MainActivity : Activity, Osmdroid.Views.Overlay.ItemizedIconOverlay.IOnItemGestureListener
    { // basic set up compared to google maps, no api key and no documentation for xamarin .
        //The wrapper has a Marker function but seems to lack the interface to support it
        
        Button b1, b2;
        int count = 1;
        private IMapController mapcontroller;
        private MapView mapview;
        
List<OverlayItem> listofmarkers = new List<OverlayItem>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
             Button b1 = FindViewById<Button>(Resource.Id.button1);
            Button b2 = FindViewById<Button>(Resource.Id.button2);
           b1.Click+=b1_Click;
            b2.Click+=b2_Click;

            setupmap();
           




            
      }

        void setupmap() 
        {
          



            //Setup Map
            mapview = FindViewById<MapView>(Resource.Id.mapview);
            mapview.SetTileSource(TileSourceFactory.DefaultTileSource); // can choose other tile provides
            mapview.SetBuiltInZoomControls(true); // +- buttons at bottom
            mapview.SetMultiTouchControls(true); // Pinch to zoom
           
            
            mapcontroller = mapview.Controller; // instantiate contoller
            mapcontroller.SetZoom(15); // set initial zoom


            mapview.HorizontalScrollBarEnabled = true; // seems not to work, might require a visual resource
                   
           
           
            DefaultResourceProxyImpl resProxy;
            resProxy = new DefaultResourceProxyImpl(this);
            

            //DRAW A POLYLINE
             
            //create some geopoints
            GeoPoint g = new GeoPoint(-37.67234, 175.2540);
            GeoPoint h = new GeoPoint(-37.873, 175.350);
            GeoPoint i = new GeoPoint(-37.123, 175.410);
            GeoPoint j = new GeoPoint(-37.373, 175.590);
            GeoPoint k = new GeoPoint(-37.453, 175.650);
            GeoPoint [] listgeopoint={g,h,i,j,k}; //seems like this has to be an array , not a list
            
            //create a pathOverlay
            PathOverlay po = new PathOverlay(Android.Graphics.Color.Red, 5f, resProxy);
            po.AddPoints(listgeopoint);
           
           //You can add them individually also, the order is important ! 
            // po.AddPoint(h);
            //po.AddPoint(i);
            //po.AddPoint(g);
            //po.AddPoint(j);

            //MARKERS
            //set up a marker
            Drawable markerDrawable;
            markerDrawable = Resources.GetDrawable(Resource.Drawable.mapicon);

            GeoPoint point = new GeoPoint(-37.77, 175.0);

            //create overlay to host marker
            OverlayItem item = new OverlayItem("Title", "Longer Text", point); // no info window, but we can access these, see event handler
            item.SetMarker(markerDrawable);

            //add marker to list
            listofmarkers.Add(item);

            //then add list of markers to Overlay
            ItemizedIconOverlay overyLay = new ItemizedIconOverlay(listofmarkers, markerDrawable, this, resProxy);
            //add overlay to mapview
            mapview.Overlays.Add(overyLay);

            //adds the polyline/path overlay
            mapview.Overlays.Add(po);
            //sets centrepoint of map(camera)
            mapcontroller.SetCenter(point);
        
 }


public bool OnItemLongPress (int index, Java.Lang.Object item) // lomg click event for marker
{

return true; // does nothing if we return true
}

public bool OnItemSingleTapUp (int index, Java.Lang.Object item) // click event for marker
{
Console.WriteLine ("You clicked" + listofmarkers[index].Title);
    
return false;
}

  void b2_Click(object sender, EventArgs e)
        {
            var gp = new GeoPoint(-38.132, 176.25400);
            mapcontroller.AnimateTo(gp); // shifts map
        }

        void b1_Click(object sender, EventArgs e)
        { var gp = new GeoPoint(-38.15242000,176.2743000);
        mapcontroller.AnimateTo(gp);// shifts map
        }



        
    }
}

