using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CheckBoxandRadioButtons
{
	[Activity (Label = "CheckBoxandRadioButtons", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		CheckBox chkBanana;
		CheckBox chkOrange;
		CheckBox chkApple;
		Button btnSelection;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it

			btnSelection = FindViewById<Button> (Resource.Id.btnSelection);
			chkBanana = FindViewById<CheckBox> (Resource.Id.chkBanana);
			chkApple = FindViewById<CheckBox> (Resource.Id.chkApple);
			chkOrange = FindViewById<CheckBox> (Resource.Id.chkOrange);

			btnSelection.Click += OnSelectionClick;

		}
			
		public void OnSelectionClick(object sender,EventArgs e)
		{
			if (chkBanana.Checked) {
				Toast.MakeText (this, "Banana Checked", ToastLength.Short).Show ();
			} 
			if (chkOrange.Checked) {
				Toast.MakeText (this, "Orange Checked", ToastLength.Short).Show ();
			} 
			if (chkApple.Checked) {
				Toast.MakeText (this, "Apple Checked", ToastLength.Short).Show ();
			}
		}
	}
}


