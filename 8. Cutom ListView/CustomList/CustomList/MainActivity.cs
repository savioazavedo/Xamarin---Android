using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CustomList
{
	[Activity (Label = "CustomList", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.Light")]

	public class MainActivity : Activity
	{
		ListView listView;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			listView = FindViewById<ListView>(Resource.Id.List); 

			List<Data> myList = new List<Data> ();

			Data obj = new Data ();
			obj.Heading = "Apple";
			obj.SubHeading = "An Apple a day keeps the doctor away";
			obj.ImageURI = "http://www.thestar.com/content/dam/thestar/opinion/editorials/star_s_view_/2011/10/12/an_apple_a_day_not_such_a_good_idea/apple.jpeg";

			myList.Add (obj);

            Data obj1 = new Data();
            obj1.Heading = "Banana";
            obj1.SubHeading = "Bananas are an excellent source of vitamin B6 ";
            obj1.ImageURI = "http://www.bbcgoodfood.com/sites/bbcgoodfood.com/files/glossary/banana-crop.jpg";

            myList.Add(obj1);

            Data obj2 = new Data();
            obj2.Heading = "Kiwi Fruit";
            obj2.SubHeading = "Kiwifruit is a rich source of vitamin C";
            obj2.ImageURI = "http://www.wiffens.com/wp-content/uploads/kiwi.png";

            myList.Add(obj2);

            Data obj3 = new Data();
            obj3.Heading = "Pineapple";
            obj3.SubHeading = "Raw pineapple is an excellent source of manganese";
            obj3.ImageURI = "http://www.medicalnewstoday.com/images/articles/276/276903/pineapple.jpg";

            myList.Add(obj3);

            Data obj4 = new Data();
            obj4.Heading = "Strawberries";
            obj4.SubHeading = "One serving (100 g)of strawberries contains approximately 33 kilocalories";
            obj4.ImageURI = "https://ecs3.tokopedia.net/newimg/product-1/2014/8/18/5088/5088_8dac78de-2694-11e4-8c99-6be54908a8c2.jpg";

            myList.Add (obj4);
            listView.Adapter = new DataAdapter(this,myList);

		}
	}
}


