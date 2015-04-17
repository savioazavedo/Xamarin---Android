using System;
using Android.App;
using Android.Runtime;
using Parse;

namespace Feedr
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
			ParseClient.Initialize("pOFmNcDC6e3KKKOgByXQYBEPVmypu7j0SDGP2k0o", "BLil2ythBR4KeWoSuwv8FG9NWWYYDSvdyYdTQhe3");
		}
	}
}