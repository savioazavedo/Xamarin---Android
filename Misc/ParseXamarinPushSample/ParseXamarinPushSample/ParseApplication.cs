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

using Parse;

namespace ParseXamarinPushSample {
  [Application(Name = "parsexamarinpushsample.ParseApplication")]
  class ParseApplication : Application {
    public ParseApplication(IntPtr handle, JniHandleOwnership ownerShip)
      : base(handle, ownerShip) {
    }

    public override void OnCreate() {
      base.OnCreate();

      ParseClient.Initialize("pOFmNcDC6e3KKKOgByXQYBEPVmypu7j0SDGP2k0o", "BLil2ythBR4KeWoSuwv8FG9NWWYYDSvdyYdTQhe3");
      ParsePush.ParsePushNotificationReceived += ParsePush.DefaultParsePushNotificationReceivedHandler;
    }
  }
}