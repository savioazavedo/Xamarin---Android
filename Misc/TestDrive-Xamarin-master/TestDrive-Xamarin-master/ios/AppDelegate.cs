﻿using System;
using System.Linq;
using System.Collections.Generic;
using KinveyXamarin;
using SQLite.Net.Platform.XamarinIOS;
using Foundation;
using UIKit;

namespace iOSTestDrive
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public Client myClient { get; set; }
		
		public override UIWindow Window {
			get;
			set;
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

		public override void FinishedLaunching(UIApplication application)
		{


			myClient = new Client.Builder ("kid_PeYFqjBcBJ", "3fee066a01784e2ab32a255151ff761b")
				.setFilePath(NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User) [0].ToString())
				.setOfflinePlatform(new SQLitePlatformIOS())
				.setLogger(delegate(string msg) { Console.WriteLine(msg);})
				.build ();
		}
	}
}

