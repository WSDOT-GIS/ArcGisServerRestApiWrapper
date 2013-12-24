using System.IO;
using System.Net;

namespace Esri.ArcGisServer.Rest.Maps
{
	/// <summary>
	/// Represents the state of an Export Map request.
	/// </summary>
	public class ExportMapRequestState
	{
		/// <summary>
		/// The parameters of the Export Map request.
		/// </summary>
		public ExportMapParameters Parameters { get; set; }
		/// <summary>
		/// The <see cref="HttpWebRequest"/>.
		/// </summary>
		public HttpWebRequest Request { get; set; }
		//public HttpWebResponse Response { get; set; }
		/// <summary>
		/// The response stream of the web request.
		/// </summary>
		public Stream ResponseStream { get; set; }
	}
}
