using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace heliosweather
{
    class DataAdapterLoactions : BaseAdapter<ClassLocations>
    {
        //Data adaptor for fillng in List view
        List<ClassLocations> items;
        Activity context;

        public DataAdapterLoactions(Activity context, List<ClassLocations> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override ClassLocations this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) //Inflate custom row for left drawer locations
                view = context.LayoutInflater.Inflate(Resource.Layout.LocationRow, null);
            //set the town name
            view.FindViewById<TextView>(Resource.Id.txtLocation_Location).Text = item.town;

            return view;
        }
    }
}