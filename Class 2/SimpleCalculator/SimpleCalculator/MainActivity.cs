using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SimpleCalculator
{
	[Activity (Label = "SimpleCalculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		TextView txtNum1;
		TextView txtNum2;

		double Num1;
		double Num2;
		double Result;

		Button btnplus;
		Button btnminus;
		Button btnmul;
		Button btndivide;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			//Button button = FindViewById<Button> (Resource.Id.myButton);

			InitializeControls ();
		}

		public void InitializeControls()
		{
			btnplus = FindViewById<Button> (Resource.Id.btnplus);
			btnminus = FindViewById<Button> (Resource.Id.btnminus);
			btnmul = FindViewById<Button> (Resource.Id.btnmul);
			btndivide = FindViewById<Button> (Resource.Id.btnDivide);

			txtNum1 = FindViewById <TextView> (Resource.Id.txtNum1);
			txtNum2 = FindViewById <TextView> (Resource.Id.txtNum2);

			btnplus.Click += onBtnPlusClick;
			btnminus.Click += onBtnMinusClick;
			btnmul.Click += onBtnMulClick;
			btndivide.Click += onBtnDivideClick;
		}

		void onBtnDivideClick (object sender, EventArgs e)
		{
			Num1 = Convert.ToDouble(txtNum1.Text);
			Num2 = Convert.ToDouble(txtNum2.Text);
			Result = Num1 / Num2;
			Toast.MakeText(this, "THE RESULT IS " + Result,ToastLength.Long).Show();
		}

		void onBtnMulClick (object sender, EventArgs e)
		{
			Num1 = Convert.ToDouble(txtNum1.Text);
			Num2 = Convert.ToDouble(txtNum2.Text);
			Result = Num1 * Num2;
			Toast.MakeText(this, "THE RESULT IS " + Result,ToastLength.Long).Show();
		}

		void onBtnMinusClick (object sender, EventArgs e)
		{
			Num1 = Convert.ToDouble(txtNum1.Text);
			Num2 = Convert.ToDouble(txtNum2.Text);
			Result = Num1 - Num2;
			Toast.MakeText(this, "THE RESULT IS " + Result,ToastLength.Long).Show();
		}

		void onBtnPlusClick (object sender, EventArgs e)
		{
			Num1 = Convert.ToDouble(txtNum1.Text);
			Num2 = Convert.ToDouble(txtNum2.Text);
			Result = Num1 + Num2;
			Toast.MakeText(this, "THE RESULT IS " + Result,ToastLength.Long).Show();
		}

	}
}


