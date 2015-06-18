using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace BusLookATour
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
			ParseClient.Initialize ("hCcUYPU6UIDE7SdeVBdp5C6w9u0t4iPGJxcDHede", "3h7OYxJu7XkRt31f3dNy448nRJ6oTbkMsm9gnmct");
		}
	}
}

