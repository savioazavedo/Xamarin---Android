using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BMICalculator
{
	[Activity (Label = "BMICalculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		EditText txtHeight;
		EditText txtWeight;
		EditText txtResult;
		TextView lblMessage;

		double Height;
		double Weight;
		double BMI;

		Button btnCalculate;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
		
			txtHeight = FindViewById <EditText> (Resource.Id.txtHeight);
			txtWeight = FindViewById <EditText> (Resource.Id.txtWeight);
			txtResult = FindViewById <EditText> (Resource.Id.txtResult);
			lblMessage = FindViewById <TextView> (Resource.Id.lblMessage);

			btnCalculate = FindViewById <Button> (Resource.Id.btnCalculate);
			btnCalculate.Click += onBtnCalculateClick;

		}


		public void onBtnCalculateClick(object sender,EventArgs e)
		{
			if (txtWeight.Text == "") 
			{
				Toast.MakeText (this, "Please enter the weight", ToastLength.Long);
				return;
			}

			if (txtHeight.Text == "") 
			{
				Toast.MakeText (this, "Please enter the height", ToastLength.Long);
				return;
			}

			Height = Convert.ToDouble(txtHeight.Text);
			Weight = Convert.ToDouble (txtWeight.Text);
			BMI = Weight / (Height * Height);
			txtResult.Text = Convert.ToString (Math.Round(BMI,2));

			if (BMI <= 18.5)
            {
				lblMessage.Text = "Underweight";							
			}
            else if (BMI >= 18.60 && BMI <= 24.99)
            {
				lblMessage.Text = "Normal";						
			}
            else if (BMI > 25 && BMI <= 29.99)
            {
				lblMessage.Text = "Overweight";	
			}
            else if (BMI > 30)
            {
				lblMessage.Text = "Obese";	
			}
				
		}


	}
}


