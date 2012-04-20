using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArcGisServerRestApiWrapper
{
	public class MapExportCompletedEventArgs : EventArgs
	{
		public Stream ResponseStream { get; set; }
		public Exception Error { get; set; }
	}
}
