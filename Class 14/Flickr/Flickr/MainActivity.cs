using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace Flickr
{
	[Activity (Label = "Flickr Interesting", MainLauncher = true, Icon = "@drawable/icon",ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
	public class MainActivity : Activity
	{
		int count = 0;

		ImageButton btnPrev;
		ImageButton btnNext;
		ImageView imgPic;

		RESThandler objRest;
		RootObject objRootList;

		List<Photo> lstPhotos = new List<Photo> ();  

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			btnPrev = FindViewById<ImageButton> (Resource.Id.btnprev);
			btnNext = FindViewById<ImageButton> (Resource.Id.btnnext);

			imgPic = FindViewById<ImageView> (Resource.Id.imageView1);
			
			btnNext.Click += OnBtnNextClick;
			btnPrev.Click += OnBtnPrevClick;

			try 
			{

				objRest = new RESThandler (@"https://api.flickr.com/services/rest/?method=flickr.interestingness.getList");

				objRest.AddParameter ("api_key","d73eead59ba6d464e63b0fe20d545e92");
				objRest.AddParameter ("format","json");
				objRest.AddParameter ("nojsoncallback","1");
				objRest.AddParameter ("api_sig","23d87dfaf266466ba924ae265a23a13e");

				objRootList = objRest.ExecuteRequest ();
				lstPhotos = objRootList.photos.photo;
			
				GetImage (count);

			} catch (Exception e) {
				Toast.MakeText(this,"Error" + e.Message,ToastLength.Long);
			}
		}


		public void GetImage(int index)
		{
			String imgurl;

			try
			{
				var tempmap = lstPhotos[index];
				imgurl = "http://farm" + tempmap.farm.ToString() + ".staticflickr.com/" + tempmap.server + "/" + tempmap.id  + "_" + tempmap.secret + "_b.jpg" ;
				Koush.UrlImageViewHelper.SetUrlDrawable (imgPic,imgurl, Resource.Drawable.loading);

			} catch (Exception e) {
				Toast.MakeText(this,"Error" + e.Message,ToastLength.Long);
			}
		}

		public void OnBtnNextClick(object sender,EventArgs e)
		{
			count = count + 1;

			if(count < 100) {
				GetImage (count);
			}
		}

		public void OnBtnPrevClick(object sender,EventArgs e)
		{
			count = count - 1;

			if(count > 0) {
				GetImage (count);
			}
		}

	}
}


