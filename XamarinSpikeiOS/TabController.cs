using System;
using CoreLocation;
using UIKit;
using System.Drawing;
using SharedProject;


namespace XamarinSpikeiOS
{
	public class TabController : UITabBarController {

		UIViewController tab1, tab2, tab3;

		public TabController ()
		{
			tab1 = new UIViewController();
			tab1.Title = "Home";
			tab1.View.BackgroundColor = UIColor.White;
			float position = (float)tab1.View.Frame.Width - 40;
			var label = new UILabel (new RectangleF(20, 40, position, 40));
			label.AdjustsFontSizeToFitWidth = true;
			label.TextColor = UIColor.Red;
			label.Text = "This app took about an hour to build";
			tab1.View.Add (label);

			tab2 = new UIViewController();
			tab2.Title = "Schedule";
			tab2.View.BackgroundColor = UIColor.White;

			Api api = new Api();
			var schedules = api.GetSchedule();
			var schedulelabel = new UILabel (new RectangleF(20, 40, position, 40));
			schedulelabel.AdjustsFontSizeToFitWidth = true;
			schedulelabel.TextColor = UIColor.Red;
			foreach(Schedule schedule in schedules){
			schedulelabel.Text += schedule.title.ToString() + " ";
			}
			tab2.View.Add (schedulelabel);

			tab3 = new UIViewController();
			tab3.Title = "Location";
			tab3.View.BackgroundColor = UIColor.White;

			if (CLLocationManager.LocationServicesEnabled) 
			{
				var LocMgr = new CLLocationManager();

				// request permission
				LocMgr.RequestWhenInUseAuthorization ();
				// Start location updates
				LocMgr.StartUpdatingLocation ();
				System.Threading.Thread.Sleep (5000);
				var locationlabel = new UILabel (new RectangleF(20, 40, position, 40));
				locationlabel.AdjustsFontSizeToFitWidth = true;
				locationlabel.TextColor = UIColor.Red;
				locationlabel.Text = string.Format("Your latitude is {0} and your longitude is {1}", LocMgr.Location.Coordinate.Latitude, LocMgr.Location.Coordinate.Longitude);
				tab3.View.Add (locationlabel);

			}

			var tabs = new UIViewController[] {
				tab1, tab2, tab3
			};

			ViewControllers = tabs;
		}
	}
}

