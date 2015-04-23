using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamrainSpike
{
	[Activity(Label = "XamarinSpike", MainLauncher = true, Icon = "@drawable/icon", Theme="@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Set our custom view
			ActionBar.SetCustomView(Resource.Layout.action_bar);
			ActionBar.SetDisplayShowCustomEnabled(true);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


		}
	}
}


