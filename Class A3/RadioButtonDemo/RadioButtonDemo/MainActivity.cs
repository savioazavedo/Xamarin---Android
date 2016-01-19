using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RadioButtonDemo
{
	[Activity (Label = "RadioButtonDemo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		RadioGroup rbGroup;
		RadioButton rbMale;
		RadioButton rbFemale;
		Button btnSelection;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it

			rbMale = FindViewById<RadioButton> (Resource.Id.rbMale);
			rbFemale = FindViewById<RadioButton> (Resource.Id.rbFemale);
			btnSelection = FindViewById<Button> (Resource.Id.btnSelection);

			rbGroup = FindViewById<RadioGroup> (Resource.Id.widget39);

			btnSelection.Click += OnButtonSelectionClick;
		}

		void OnButtonSelectionClick(object sender,EventArgs e)
		{
			if (rbMale.Checked) {
				Toast.MakeText (this, "Male selected", ToastLength.Long).Show();	
			} else if (rbFemale.Checked) {
				Toast.MakeText (this, "Female selected", ToastLength.Long).Show();
			}

			View v = rbGroup.GetChildAt (0);
			RadioButton test = (RadioButton)v;
			test.Text = "Test";
		}

	}
}


