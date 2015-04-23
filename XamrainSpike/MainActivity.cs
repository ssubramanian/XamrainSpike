using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Locations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XamrainSpike
{
	[Activity(Label = "XamarinSpike", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : FragmentActivity, ILocationListener
	{
		Location _currentLocation;
		LocationManager _locationManager;
		String _locationProvider;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

			var pager = FindViewById<ViewPager>(Resource.Id.pager);
			var adaptor = new GenericFragmentPagerAdaptor(SupportFragmentManager);
			InitializeLocationManager();

			adaptor.AddFragmentView((i, v, b) =>
				{
					var view = i.Inflate(Resource.Layout.tab, v, false);
					var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);
					sampleTextView.Text = "This app has taken more than 5 hours and counting..";
					return view;
				}
			);

			adaptor.AddFragmentView((i, v, b) =>
				{
					var view = i.Inflate(Resource.Layout.tab, v, false);
					var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);
					var schedules = GetSchedule();

					foreach(Schedule schedule in schedules){
						sampleTextView.Text += schedule.title.ToString() + " ";
					}

					return view;
				}
			);

			adaptor.AddFragmentView((i, v, b) =>
				{
					var view = i.Inflate(Resource.Layout.tab, v, false);
					var sampleTextView = view.FindViewById<TextView>(Resource.Id.textView1);

					var address = GetAddress();

					sampleTextView.Text = address;
					return view;
				}
			);

			pager.Adapter = adaptor;
			pager.SetOnPageChangeListener(new ViewPageListenerForActionBar(ActionBar));

			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Home"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Schedule"));
			ActionBar.AddTab(pager.GetViewPageTab(ActionBar, "Location"));
		}

		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
			
		}

		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
		}

		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		List<Schedule> GetSchedule()
		{
			// Get the latitude and longitude entered by the user and create a query.
			string url = "http://staging.activelifeadmin.com/dummy/websearch/public/index/getscheduleslist?branch_ids=3";

			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";

			// Send the request to the server and wait for the response:
			using (WebResponse response = request.GetResponse ())
			{
				// Get a stream representation of the HTTP web response:
				using (StreamReader reader = new StreamReader(response.GetResponseStream ()))
				{
					string content = reader.ReadToEnd();
					List<Schedule> schedules = JsonConvert.DeserializeObject<List<Schedule>> (content);

					return schedules;
				}
			}
		}

		string GetAddress()
		{
			if (_currentLocation == null)
			{
				return "Cannot determine the current address.";
			}

			Geocoder geocoder = new Geocoder(this);
			IList<Address> addressList = geocoder.GetFromLocation(_currentLocation.Latitude, _currentLocation.Longitude, 10);

			Address address = addressList.FirstOrDefault();
			if (address != null)
			{
				StringBuilder deviceAddress = new StringBuilder();
				for (int i = 0; i < address.MaxAddressLineIndex; i++)
				{
					deviceAddress.Append(address.GetAddressLine(i))
						.AppendLine(",");
				}
				return deviceAddress.ToString();
			}
			else
			{
				return "Unable to determine the address.";
			}
		}


		void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = String.Empty;
			}
		}
	}

	public class Schedule
	{
		public string sched_type_id { get; set; }
		public string title { get; set; }
	}
}


