
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

namespace NZRoadCode
{
	[Activity (Label = "NZ Road Code Test")]			
	public class TestActivity : Activity
	{
		TextView txtQuestion;
		ImageView imPic;
		RadioButton rbA;
		RadioButton rbB;
		RadioButton rbC;
		RadioButton rbD;
		Button btnNext;
		DatabaseManager objDb;
		List<DrivingTest> myList;

		DrivingTest objTest;
	
		int rowid = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Test);

			rbA = FindViewById<RadioButton> (Resource.Id.rbA);
			rbB = FindViewById<RadioButton> (Resource.Id.rbB);
			rbC = FindViewById<RadioButton> (Resource.Id.rbC);
			rbD = FindViewById<RadioButton> (Resource.Id.rbD);

			btnNext = FindViewById<Button> (Resource.Id.btnNext);

			txtQuestion = FindViewById<TextView> (Resource.Id.txtQuestion);
			imPic = FindViewById<ImageView> (Resource.Id.imPic);

			btnNext.Click += LoadNextQuestion;

			objDb = new DatabaseManager ();
			objTest = new DrivingTest ();

			GetAllQuestions ();
		}

		public void GetAllQuestions()
		{
			myList = objDb.GetQuestion();
			LoadQuestion ();
		}

		public void LoadQuestion()
		{
			objTest = myList [rowid];
			txtQuestion.Text = objTest.Question;

			rbA.Text = objTest.A;
			rbB.Text = objTest.B;
			rbC.Text = objTest.C;
			rbD.Text = objTest.D;

			if (objTest.pic != "") {
				loadPic ();
			}
		}
			
		public void LoadNextQuestion(object sender,EventArgs e)
		{
			if (rowid < 34) {
				rowid = rowid + 1;
				LoadQuestion ();
				CheckAnswer ();

			} else {
				StartActivity (typeof(ScoreActivity));
			}
		}
			
		public void loadPic()
		{
			if(objTest.pic == "pic1.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic1);
			}

			if(objTest.pic == "pic2.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic2);
			}

			if(objTest.pic == "pic3.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic3);
			}

			if(objTest.pic == "pic4.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic4);
			}

			if(objTest.pic == "pic5.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic5);
			}

			if(objTest.pic == "pic6.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic6);
			}

			if(objTest.pic == "pic7.jpg")
			{
				imPic.SetImageResource (Resource.Drawable.Pic7);
			}
		}

		public void CheckAnswer()
		{
			// To-Do 
			// Check Answer is correct
			//Increment Score ...
		}

	}
}

