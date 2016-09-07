using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics.Drawables;
using System.IO;
using Android.Graphics;
using System.Net;
using System.Text;


namespace Flickr
{
	[Activity (Label = "Flickr Interesting", MainLauncher = true, Icon = "@drawable/icon",ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
	public class MainActivity : Activity
	{
		int count = 0;

		ImageButton btnPrev;
		ImageButton btnNext;
		ImageButton btnSave;
		ImageButton btnShare;
		TextView txtCaption;
		Random imageID;
		ImageView imgPic;

		RESThandler objRest;
		RootObject objRootList;

		double photoID;

		List<Photo> lstPhotos = new List<Photo> ();  

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			btnPrev = FindViewById<ImageButton> (Resource.Id.btnprev);
			btnNext = FindViewById<ImageButton> (Resource.Id.btnnext);
			btnSave = FindViewById<ImageButton> (Resource.Id.btnSave);
			btnShare = FindViewById<ImageButton> (Resource.Id.btnShare);
			txtCaption = FindViewById<TextView> (Resource.Id.textView1);

			imgPic = FindViewById<ImageView> (Resource.Id.imageView1);
			
			btnNext.Click += OnBtnNextClick;
			btnPrev.Click += OnBtnPrevClick;
			btnSave.Click += OnBtnSaveClick;
			btnShare.Click += OnShareBtnClick;

			try 
			{

				objRest = new RESThandler (@"https://api.flickr.com/services/rest/?method=flickr.interestingness.getList");


				// get the latest api_key and api_sig from flickr 
				objRest.AddParameter ("api_key", "2f9557344467c41a762ab15727fd46a1");
				objRest.AddParameter ("format","json");
				objRest.AddParameter ("nojsoncallback","1");
				objRest.AddParameter ("api_sig", "fe8dd5331a2245ad073db8f3dff73c75");

				objRootList = objRest.ExecuteRequest ();
				lstPhotos = objRootList.photos.photo;
			
				GetImage (count);

			} catch (Exception e) {
				Toast.MakeText(this,"Error" + e.Message,ToastLength.Long);
			}
		}

		void OnShareBtnClick (object sender, EventArgs e)
		{
			// Share click using Intents 

			try
			{

				string localPath =System.IO.Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), photoID + ".jpeg");
				Intent shareIntent = new Intent (Intent.ActionSend);
				shareIntent.SetType ("image/*");
				Android.Net.Uri uri =  Android.Net.Uri.Parse(localPath);
				shareIntent.PutExtra (Intent.ExtraStream, uri);
				StartActivity(Intent.CreateChooser(shareIntent, "Share photo"));

			} catch (Exception ex) {
				Toast.MakeText(this,"Error" + ex.Message,ToastLength.Long);
			}

		}

		void OnBtnSaveClick (object sender, EventArgs e)
		{
			imageID = new Random ();
			photoID = imageID.Next (999999999); 

			var fetchedDrawable = imgPic.Drawable;
			BitmapDrawable bitmapDrawable = (BitmapDrawable)fetchedDrawable;
			var bitmap = bitmapDrawable.Bitmap;

			byte[] bitmapData;
			using (var stream = new MemoryStream())
			{
				bitmap.Compress(Bitmap.CompressFormat.Jpeg ,100, stream);
				bitmapData = stream.ToArray();
			}

			try
			{
				string localPath =System.IO.Path.Combine (Android.OS.Environment.ExternalStorageDirectory.ToString (), photoID + ".jpeg");
				File.WriteAllBytes (localPath, bitmapData); 
				Toast.MakeText(this,"Image Saved",ToastLength.Short).Show();

			} catch (Exception ex) {
				Toast.MakeText(this,"Failed to save image:" + ex.Message,ToastLength.Short).Show();
			}
		}


		public void GetImage(int index)
		{
			String imgurl;

			try
			{
				var tempmap = lstPhotos[index];
				imgurl = "http://farm" + tempmap.farm.ToString() + ".staticflickr.com/" + tempmap.server + "/" + tempmap.id  + "_" + tempmap.secret + "_b.jpg" ;
				txtCaption.Text = tempmap.title;
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


