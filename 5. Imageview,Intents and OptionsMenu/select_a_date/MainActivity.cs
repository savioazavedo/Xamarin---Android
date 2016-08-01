using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using Android.Provider;
using Java.Util;

namespace com.xamarin.sample.datepicker
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView txtEventStartDate;
        TextView txtEventEndDate;
        TextView txtEventDescription;
        Button StartDateSelectButton;
        Button EndDateSelectButton;
        Button btnCalendarAdd;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            txtEventStartDate = FindViewById<TextView>(Resource.Id.date_display);
            txtEventEndDate = FindViewById<TextView>(Resource.Id.txtDateEnd);
            txtEventDescription = FindViewById<TextView>(Resource.Id.editText1);
            StartDateSelectButton = FindViewById<Button>(Resource.Id.date_select_button);
            EndDateSelectButton = FindViewById<Button>(Resource.Id.date_select_button2);
            btnCalendarAdd = FindViewById<Button>(Resource.Id.btnCalendar);

            StartDateSelectButton.Click += StartDateSelectButton_Click;
            EndDateSelectButton.Click += EndDateSelectButton_Click;
            btnCalendarAdd.Click += BtnCalendarAdd_Click;
        }

        private void BtnCalendarAdd_Click(object sender, EventArgs e)
        {
            ContentValues eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId,1);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title,"Testing Calendar");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, txtEventDescription.Text);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart,GetDateTimeMS(2016, 12, 15, 10, 0));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend,GetDateTimeMS(2016, 12, 15, 11, 0));

            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone,"UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone,"UTC");

            var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri,eventValues);
            Console.WriteLine("Uri for new event: {0}", uri);
        }

        private void EndDateSelectButton_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtEventEndDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }


        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Calendar.DayOfMonth, 15);
            c.Set(Calendar.HourOfDay, hr);
            c.Set(Calendar.Minute, min);
            c.Set(Calendar.Month, Calendar.December);
            c.Set(Calendar.Year, 2011);

            return c.TimeInMillis;
        }



        void StartDateSelectButton_Click(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate(DateTime time)
                                                                     {
                                                                         txtEventStartDate.Text = time.ToLongDateString();
                                                                     });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}