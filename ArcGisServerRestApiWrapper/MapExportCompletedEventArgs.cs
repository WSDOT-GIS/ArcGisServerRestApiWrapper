using System;
using System.IO;

namespace Esri.ArcGisServer.Rest
{
	public class MapExportCompletedEventArgs : EventArgs
	{
		public ExportMapParameters Parameters { get; set; }
		public Stream ResponseStream { get; set; }
		public Exception Error { get; set; }
	}
}
