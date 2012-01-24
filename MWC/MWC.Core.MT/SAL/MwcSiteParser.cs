using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using MWC.BL;

namespace MWC.SAL
{
	public class MWCSiteParser
	{
		string _url;
		static MWCSiteParser ()
		{
		}
		public MWCSiteParser ()
		{
		}
		public Conference ConferenceData { get; set; }
		public List<Exhibitor> Exhibitors { get; set; }

		public void GetConference (string url, Action action)
		{			
			var webClient = new WebClient ();
			Debug.WriteLine ("Get remote data for conference");
			webClient.DownloadStringCompleted += (sender, e) =>
			{
				try 
				{
					var r = e.Result;
					ConferenceData = DeserializeConference (r);
				} catch (Exception ex) {
					Debug.WriteLine ("ERROR deserializing downloaded XML: " + ex);
				}
				action();
			};
			webClient.Encoding = System.Text.Encoding.UTF8;
			webClient.DownloadStringAsync (new Uri (url));
		}

		public void GetExhibitors (string url, Action action)
		{			
			var webClient = new WebClient ();
			Debug.WriteLine ("Get remote data for conference");
			webClient.DownloadStringCompleted += (sender, e) =>
			{
				try 
				{
					var r = e.Result;
					ConferenceData = DeserializeConference (r);
				} catch (Exception ex) {
					Debug.WriteLine ("ERROR deserializing downloaded XML: " + ex);
				}
				action();
			};
			webClient.Encoding = System.Text.Encoding.UTF8;
			webClient.DownloadStringAsync (new Uri (url));
		}
		
		internal static Conference DeserializeConference (string xml)
		{
			Conference confData = null;
			try {
				var serializer = new XmlSerializer(typeof(Conference));
				var sr = new StringReader(xml);
				confData = (Conference)serializer.Deserialize(sr);
			} catch (Exception ex) {
				Debug.WriteLine ("ERROR deserializing downloaded conference XML: " + ex);
			}
			return confData;
		}
		
		internal static List<Exhibitor> DeserializeExhibitors (string xml)
		{
			List<Exhibitor> exhibitorData = null;
			try {
				var serializer = new XmlSerializer(typeof(List<Exhibitor>));
				var sr = new StringReader(xml);
				exhibitorData = (List<Exhibitor>)serializer.Deserialize(sr);
			} catch (Exception ex) {
				Debug.WriteLine ("ERROR deserializing downloaded exhibitor XML: " + ex);
			}
			return exhibitorData;
		}
	}
}