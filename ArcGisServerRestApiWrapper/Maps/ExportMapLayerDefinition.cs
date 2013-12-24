namespace Esri.ArcGisServer.Rest.Maps
{
	

	/// <summary>
	/// An layer definition for the export map operation.
	/// </summary>
	public class ExportMapLayerDefinition
	{
		/// <summary>The integer that identifies a layer of the map service.</summary>
		public int LayerId { get; set; }
		/// <summary>Query that will executed on the layer.</summary>
		public string Query { get; set; }

		/// <summary>Converts to a string representation.</summary>
		/// <returns>Layer ID and query separated by a colon character.</returns>
		public override string ToString()
		{
			return string.Format("{0}:{1}", LayerId, Query);
		}
	}

	
}
