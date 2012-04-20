using System;

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
		public int? Wkid { get; set; }

		/// <summary>
		/// Well-Known Text (WKT)
		/// </summary>
		public string Wkt { get; set; }

		/// <summary>
		/// Creates a new <see cref="SpatialReference"/>
		/// </summary>
		/// <param name="wkid">Well-Known ID of a spatial reference.</param>
		public SpatialReference(int wkid)
		{
			Wkid = wkid;
		}

		/// <summary>
		/// Creates a new <see cref="SpatialReference"/>
		/// </summary>
		/// <param name="wkt">Well-Known Text</param>
		public SpatialReference(string wkt)
		{
			Wkt = wkt;
		}

		/// <summary>
		/// Converts the object into a JSON string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (Wkid.HasValue)
			{
				return string.Format("{{\"wkid\":{0}}}", Wkid);
			}
			else if (!string.IsNullOrWhiteSpace(Wkt))
			{
				return string.Format("{{\"wkt\":{0}}}", Wkt);
			}
			else
			{
				throw new NotSupportedException("A value must be provided for either the Wkid or Wkt parameter.");
			}
		}
	}
}
