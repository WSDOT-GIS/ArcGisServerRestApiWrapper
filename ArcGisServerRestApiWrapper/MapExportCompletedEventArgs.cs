using System;
using System.IO;

namespace Esri.ArcGisServer.Rest
{
	/// <summary>
	/// Event arguments for when the Map Export operation has completed.
	/// </summary>
	public class MapExportCompletedEventArgs : EventArgs
	{
		/// <summary>
		/// The map export parameters.
		/// </summary>
		public ExportMapParameters Parameters { get; set; }
		/// <summary>
		/// The response <see cref="Stream"/>.
		/// </summary>
		public Stream ResponseStream { get; set; }
		/// <summary>
		/// If the request returned an error, this will have a value.
		/// </summary>
		public Exception Error { get; set; }
	}
}
