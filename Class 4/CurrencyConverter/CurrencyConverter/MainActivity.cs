using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CurrencyConverter
{
	[Activity (Label = "CurrencyConverter", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		Spinner spFromCurrency;
		Spinner spToCurrency;
		EditText txtFromAmount;
		EditText txtToAmount;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			txtFromAmount = FindViewById<EditText> (Resource.Id.txtFromAmount);
			txtToAmount = FindViewById<EditText> (Resource.Id.txtToAmount);
			spFromCurrency = FindViewById<Spinner> (Resource.Id.spFromCurrency);
			spToCurrency = FindViewById<Spinner> (Resource.Id.spToCurrency);

			var adapter = ArrayAdapter.CreateFromResource (this, Resource.Array.Currency, Android.Resource.Layout.SimpleSpinnerItem);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);

			spFromCurrency.Adapter = adapter;
			spToCurrency.Adapter = adapter;

			// Adding event handlers 
			spFromCurrency.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spFromCurrency_ItemSelected);
			spToCurrency.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spToCurrency_ItemSelected);		
		}

	
		public void spFromCurrency_ItemSelected(object sender,AdapterView.ItemSelectedEventArgs e)
		{

		}

		public void spToCurrency_ItemSelected(object sender,AdapterView.ItemSelectedEventArgs e)
		{
			if (spFromCurrency.SelectedItem.ToString() == "NZD" && spToCurrency.SelectedItem.ToString() == "AUD") {
				txtToAmount.Text  = Convert.ToString(Convert.ToDouble(txtFromAmount.Text) * 0.93);							
			} else if (spFromCurrency.SelectedItem.ToString() == "NZD" && spToCurrency.SelectedItem.ToString() == "USD") {
				txtToAmount.Text =  Convert.ToString(Convert.ToDouble(txtFromAmount.Text) * 0.74);	
			}
		}

	}
}


