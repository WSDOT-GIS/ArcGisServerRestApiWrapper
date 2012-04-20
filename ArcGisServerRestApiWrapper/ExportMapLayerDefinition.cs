namespace Esri.ArcGisServer.Rest
{
	

	/// <summary>
	/// An layer definition for the export map operation.
	/// </summary>
	public class ExportMapLayerDefinition
	{
		public int LayerId { get; set; }
		public string Query { get; set; }

		public override string ToString()
		{
			return string.Format("{0}:{1}", LayerId, Query);
		}
	}

	
}
