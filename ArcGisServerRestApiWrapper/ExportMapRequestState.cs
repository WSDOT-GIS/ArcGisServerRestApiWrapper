using System.IO;
using System.Net;

namespace Esri.ArcGisServer.Rest
{
	public class ExportMapRequestState
	{
		public ExportMapParameters Parameters { get; set; }
		public HttpWebRequest Request { get; set; }
		//public HttpWebResponse Response { get; set; }
		public Stream ResponseStream { get; set; }
	}
}
