using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace XamarinSpikeiOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		TabController tabController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var tabController = new TabController ();
			window.RootViewController = tabController;

			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

