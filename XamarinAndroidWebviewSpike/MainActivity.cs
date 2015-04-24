﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;
using SharedProject;

namespace XamarinAndroidWebviewSpike
{
	[Activity (Label = "XamarinAndroidWebviewSpike", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var webView = FindViewById<WebView> (Resource.Id.webView);
			webView.Settings.JavaScriptEnabled = true;

			// Use subclassed WebViewClient to intercept hybrid native calls
			webView.SetWebViewClient (new HybridWebViewClient ());

			// Render the view from the type generated from RazorView.cshtml
			var model = new Model1 () { Text = "This app took 10 mins" };
			var template = new RazorView () { Model = model };
			var page = template.GenerateString ();

			// Load the rendered HTML into the view with a base URL 
			// that points to the root of the bundled Assets folder
			webView.LoadDataWithBaseURL ("file:///android_asset/", page, "text/html", "UTF-8", null);

		}

		private class HybridWebViewClient : WebViewClient
		{
			public override bool ShouldOverrideUrlLoading (WebView webView, string url)
			{

				// If the URL is not our own custom scheme, just let the webView load the URL as usual
				var scheme = "hybrid:";

				if (!url.StartsWith (scheme))
					return false;

				// This handler will treat everything between the protocol and "?"
				// as the method name.  The querystring has all of the parameters.
				var resources = url.Substring (scheme.Length).Split ('?');
				var method = resources [0];
				var parameters = System.Web.HttpUtility.ParseQueryString (resources [1]);


				switch (method)
				{
				case "Home":
					update_text(webView, "This app took 10 mins");
					break;
				case "Schedule":
					{
						Api api = new Api();
						var schedules = api.GetSchedule();
						string schedule_text = string.Empty;

						foreach(Schedule schedule in schedules){
							schedule_text += schedule.title.ToString() + " ";
						}
						update_text (webView, schedule_text);
						break;
					}
				case "Location":
					update_text(webView, "TODO");
					break;
				}

				return true;
			}

			void update_text(WebView webView, string text)
			{
				
				// Build some javascript using the C#-modified result
				var js = string.Format ("SetLabelText('{0}');", text);

				webView.LoadUrl ("javascript:" + js);
			}
		}
	}
}

