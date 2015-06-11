using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace ParseToDo
{
	[Application]
	public class App : Application
	{

		public App (IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// Initialize the Parse client with your Application ID and .NET Key found on
			// your Parse dashboard
			ParseClient.Initialize("FS89tYpihCDsmsDLXGa3onAzUbL1eTfO2R3aePO0", "rjIhcoKg47uTmCFTiQaZud1qtiPTIAb4GPOsUTB5");
		}
	}
}