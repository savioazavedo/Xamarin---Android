//
//using System;
//using System.Collections.Generic;
//using Android.App;
//using Android.Content;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.OS;
//using Android.Graphics.Bitmap;

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
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;


namespace EventFinda
{

	public class DataAdapter : BaseAdapter<Event> {

		List<Event> items;

		Activity context;
		public DataAdapter(Activity context, List<Event> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Event this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override  View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);


			string cdata = "<![CDATA[";
			string cdata2 = "]]>";

			if (item.Name.Contains (cdata) && item.Name.Contains (cdata2)) {
				string temp = item.Name.Replace (cdata, "");
				string final = temp.Replace (cdata2, "");
				view.FindViewById<TextView>(Resource.Id.txtEventTitle).Text = final;
			} else {
				view.FindViewById<TextView>(Resource.Id.txtEventTitle).Text = item.Name;
			}

			if (item.Address.Contains (cdata) && item.Address.Contains (cdata2)) {
				string temp = item.Name.Replace (cdata, "");
				string final = temp.Replace (cdata2, "");
				view.FindViewById<TextView>(Resource.Id.txtAddress).Text = final;
			} else {
				view.FindViewById<TextView>(Resource.Id.txtAddress).Text = item.Address;
			}

			view.FindViewById<TextView> (Resource.Id.txtDate).Text = item.Datetime_start;

			if (item.Category.Name.Contains (cdata) && item.Category.Name.Contains (cdata2)) {
				string temp = item.Name.Replace (cdata, "");
				string final = temp.Replace (cdata2, "");
				view.FindViewById<TextView>(Resource.Id.txtCategory).Text = final;
			} else {
				view.FindViewById<TextView> (Resource.Id.txtCategory).Text = item.Category.Name;
			}

			if (item.Images.Image!= null) {
				var imageBitmap = GetImageBitmapFromUrl (item.Images.Image[0].Transforms.Transform[0].Url);
				view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (imageBitmap);
			}
			return view;
		}

		private   Bitmap GetImageBitmapFromUrl(string url)
		{
			try{
			Bitmap imageBitmap = null;
			if(!(url=="null"))
				using (var webClient = new WebClient())
				{
					var imageBytes = webClient.DownloadData(url);
					if (imageBytes != null && imageBytes.Length > 0)
					{
						imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
					}
						return imageBitmap;
					}
									
			} catch (Exception e) {
				System.Console.WriteLine ("Error:" + e.Message);

			}
			return null;
		}
	}
	}

