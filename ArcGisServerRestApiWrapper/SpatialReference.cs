
namespace Esri.ArcGisServer.Rest
{
	/// <summary>
	/// Represents a spatial reference system.
	/// </summary>
	public class SpatialReference
	{
		/// <summary>
		/// Well-Known Identifier (WKID)
		/// </summary>
		public int? wkid { get; set; }

		/// <summary>
		/// Well-Known Identifier (WKID)
		/// </summary>
		public int? latestWkid { get; set; }
		/// <summary>
		/// Well-Known Identifier (WKID)
		/// </summary>
		public int? vcsWkid { get; set; }
		/// <summary>
		/// Well-Known Identifier (WKID)
		/// </summary>
		public int? latestVcsWkid { get; set; }



		/// <summary>
		/// Well-Known Text (WKT)
		/// </summary>
		public string wkt { get; set; }
	}
}
