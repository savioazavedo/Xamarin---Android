using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AlertDialogDemo
{
    [Activity(Label = "AlertDialogDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            Button button3 = FindViewById<Button>(Resource.Id.button3);

            button1.Click += alertclick1;
            button2.Click += alertclick2;
            button3.Click += alertclick3;
            
        }

        private void alertclick3(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

            AlertDialog altDg = builder.Create();
            altDg.SetTitle("Alert Title");
            altDg.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            altDg.SetMessage("Sample Message");

            altDg.SetButton("Cancel", (s, ev) => {

                //Do Something 

            });

            altDg.SetButton2("Yes", (s, ev) => {

                //Do Something 

            });

            altDg.SetButton3("No", (s, ev) => {

                //Do Something 

            });
            altDg.Show();
        }

        private void alertclick2(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

            AlertDialog altDg = builder.Create();
            altDg.SetTitle("Alert Title");
            altDg.SetIcon(Resource.Drawable.Icon);
            altDg.SetMessage("Sample Message");

            altDg.SetButton("Yes", (s, ev) => {

                //Do Something 

            });

            altDg.SetButton2("No", (s, ev) => {

                //Do Something 

            });
            altDg.Show();
        }

        private void alertclick1(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);

            AlertDialog altDg = builder.Create();
            altDg.SetTitle("Alert Title");
            altDg.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            altDg.SetMessage("Sample Message");
                
            altDg.SetButton("OK", (s,ev) => {

                //Do Something 

            });
            altDg.Show();
        }
    }
}

