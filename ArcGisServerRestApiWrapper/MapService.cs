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

		protected class RequestState
		{
			public HttpWebRequest Request { get; set; }
			//public HttpWebResponse Response { get; set; }
			public Stream ResponseStream { get; set; }
		}

		public HttpWebRequest CreateExportMapWebRequest(ExportMapParameters parameters)
		{
			UriBuilder builder = new UriBuilder(string.Join("/", this.Uri, "export"));
			builder.Query = parameters.ToString();
			var request = (HttpWebRequest)HttpWebRequest.Create(builder.Uri);
			return request;
		}

		public Stream ExportMap(ExportMapParameters parameters)
		{
			var request = CreateExportMapWebRequest(parameters);
			var response = request.GetResponse();
			var stream = response.GetResponseStream();
			
			return stream;
		}

		protected void OnExportMapComplete(IAsyncResult asyncResult)
		{
			var requestState = (RequestState)asyncResult.AsyncState;
			var response = requestState.Request.EndGetResponse(asyncResult);
			requestState.ResponseStream = response.GetResponseStream();

			if (this.ExportMapCompleted != null)
			{
				var args = new MapExportCompletedEventArgs
				{
					ResponseStream = requestState.ResponseStream
				};
				this.ExportMapCompleted.Invoke(this, args);
			}
		}


		public IAsyncResult BeginExportMap(ExportMapParameters parameters)
		{
			var request = CreateExportMapWebRequest(parameters);
			var state = new RequestState { Request = request };
			return request.BeginGetResponse(OnExportMapComplete, state);
		}

		public event EventHandler<MapExportCompletedEventArgs> ExportMapCompleted;
	}
}
