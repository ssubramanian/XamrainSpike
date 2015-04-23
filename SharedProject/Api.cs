using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharedProject
{
	public class Api
	{
		public Api ()
		{
		}


		public List<Schedule> GetSchedule()
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
	}
}

