using System;
using System.IO;
using System.Net;

namespace Esri.ArcGisServer.Rest.Maps
{
	/// <summary>
	/// A class that represents a map service.
	/// </summary>
	public class MapService
	{
		/// <summary>
		/// The URI to the map service's REST API endpoint.
		/// </summary>
		public Uri Uri { get; set; }


		/// <summary>
		/// Creates an <see cref="HttpWebRequest"/> for the map service export map operation.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public HttpWebRequest CreateExportMapWebRequest(ExportMapParameters parameters)
		{
			UriBuilder builder = new UriBuilder(string.Join("/", this.Uri, "export"));
			builder.Query = parameters.ToString();
			var request = (HttpWebRequest)HttpWebRequest.Create(builder.Uri);
			return request;
		}

		/// <summary>
		/// Synchronously executes the "export map" operation and returns the response stream.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns>A <see cref="Stream"/>.  The type of data in the stream will depend on the <paramref name="parameters"/>.</returns>
		public Stream ExportMap(ExportMapParameters parameters)
		{
			var request = CreateExportMapWebRequest(parameters);
			var response = request.GetResponse();
			var stream = response.GetResponseStream();
			
			return stream;
		}

		/// <summary>
		/// Begins an asynchronous "Export Map" operation.
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns>
		/// Returns an <see cref="IAsyncResult"/>.  The <see cref="IAsyncResult.AsyncState"/> value is an <see cref="ExportMapRequestState"/> object.
		/// </returns>
		public IAsyncResult BeginExportMap(ExportMapParameters parameters)
		{
			var request = CreateExportMapWebRequest(parameters);
			var state = new ExportMapRequestState { 
				Parameters = parameters,
				Request = request
			};
			return request.BeginGetResponse(OnExportMapComplete, state);
		}

		/// <summary>
		/// This is the method that is called when the asynchronous web request completes.
		/// It triggers the <see cref="MapService.ExportMapCompleted"/> event if handlers have been defined for it.
		/// </summary>
		/// <param name="asyncResult">
		/// Returns an <see cref="IAsyncResult"/>.  The <see cref="IAsyncResult.AsyncState"/> value is an <see cref="ExportMapRequestState"/> object.
		/// </param>
		/// <exception cref="InvalidCastException">
		/// Will be thrown if <paramref name="asyncResult"/>'s <see cref="IAsyncResult.AsyncState"/> is not an <see cref="ExportMapRequestState"/>.
		/// </exception>
		protected void OnExportMapComplete(IAsyncResult asyncResult)
		{
			var requestState = (ExportMapRequestState)asyncResult.AsyncState;
			var response = requestState.Request.EndGetResponse(asyncResult);
			requestState.ResponseStream = response.GetResponseStream();

			if (this.ExportMapCompleted != null)
			{
				var args = new MapExportCompletedEventArgs
				{
					Parameters = requestState.Parameters,
					ResponseStream = requestState.ResponseStream
				};
				this.ExportMapCompleted.Invoke(this, args);
			}
		}

		/// <summary>
		/// This event is triggered when the <see cref="MapService.BeginExportMap"/> operation has completed.
		/// </summary>
		public event EventHandler<MapExportCompletedEventArgs> ExportMapCompleted;
	}
}
