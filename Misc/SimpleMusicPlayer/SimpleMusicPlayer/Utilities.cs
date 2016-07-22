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

namespace SimpleMusicPlayer
{
    class Utilities
    {
        public string Msectotime(double Time)
        {
            string Final = "";
            string Second= "";

            int hours = Convert.ToInt16(Time / (1000 * 60 * 60));
            int minutes = Convert.ToInt16((Time % (1000 * 60 * 60)) / (1000 * 60) );

            int seconds = Convert.ToInt16(((Time % (1000 * 60 * 60)) % (1000 * 60) / 1000));
            // Add hours if there
            if (hours > 0)
            {
                Final = hours.ToString()+ ":";
            }

            // Prepending 0 to seconds if it is one digit
            if (seconds < 10)
            {
                Second = "0" + seconds.ToString();
            }
            else
            {
                Second = "" + seconds.ToString();
            }

            Final = Final + minutes + ":" + Second;

            // return timer string
            return Final;
        }

        public int getProgressPercentage(long currentDuration, long totalDuration)
        {
            Double percentage = 0.0;

            long currentSeconds = Convert.ToInt64(currentDuration / 1000);
            long totalSeconds = Convert.ToInt64(totalDuration / 1000);

            // calculating percentage
            percentage = (((double)currentSeconds) / totalSeconds) * 100;

            // return percentage
            return Convert.ToInt16(percentage);
        }

        public int progressToTimer(int progress, int totalDuration)
        {
            int currentDuration = 0;
            totalDuration = Convert.ToInt16(totalDuration / 1000);
            currentDuration = Convert.ToInt16((((double)progress) / 100) * totalDuration);

            // return current duration in milliseconds
            return currentDuration * 1000;
        }



    }
}