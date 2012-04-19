using System;

namespace ArcGisServerRestApiWrapper
{
	public class SpatialReference
	{
		public int? Wkid { get; set; }
		public string Wkt { get; set; }

		public SpatialReference(int wkid)
		{
			Wkid = wkid;
		}

		public SpatialReference(string wkt)
		{
			Wkt = wkt;
		}

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
