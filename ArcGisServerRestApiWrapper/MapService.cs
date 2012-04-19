using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ArcGisServerRestApiWrapper
{
	public class MapService
	{
		public Uri Uri { get; set; }

		public Stream ExportMap(ExportMapParameters parameters)
		{
			UriBuilder builder = new UriBuilder(string.Join("/", this.Uri, "export"));
			builder.Query = parameters.ToString();
			var request = (HttpWebRequest)HttpWebRequest.Create(builder.Uri);
			var response = request.GetResponse();
			var stream = response.GetResponseStream();
			
			return stream;
		}
	}
}
