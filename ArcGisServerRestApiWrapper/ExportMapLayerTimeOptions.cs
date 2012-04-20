using System.Collections.Generic;

namespace Esri.ArcGisServer.Rest
{
	/// <summary>
	/// Represents the layer time options of the Export Map operation.
	/// </summary>
	public class ExportMapLayerTimeOptions
	{
		/// <summary>
		/// The ID of the layer that these options will control.
		/// </summary>
		public int LayerId { get; set; }
		/// <summary>
		/// Indicates if time will be used with this layer.
		/// </summary>
		public bool UseTime { get; set; }

		/// <summary>
		/// Indicates if time data will be cumulative.
		/// </summary>
		public bool? TimeDataCumulative { get; set; }

		/// <summary>
		/// The time offset to use with this layer (if any).
		/// </summary>
		public double? TimeOffset { get; set; }

		/// <summary>
		/// The measurement units of the <see cref="ExportMapLayerTimeOptions.TimeOffset"/>.
		/// </summary>
		public TimeUnit? TimeOffsetUnits { get; set; }

		/// <summary>
		/// Converts the object to the format used in a query string.
		/// </summary>
		/// <returns>The query string representation of the <see cref="ExportMapLayerTimeOptions"/></returns>
		public override string ToString()
		{
			if (!UseTime)
			{
				return LayerId + ": {\"useTime\":false}";
			}
			else
			{
				List<string> options = new List<string>();
				options.Add(string.Format("\"useTime\":{0}", UseTime));
				options.Add(string.Format("\"timeDataCumulative\":{0}", TimeDataCumulative.GetValueOrDefault(false)));
				if (TimeOffset.HasValue)
				{
					options.Add(string.Format("\"timeOffsetOffset\":{0}", TimeOffset.GetValueOrDefault()));
					options.Add(string.Format("\"timeOffsetUnits\":esriTimeUnits{0}", TimeOffsetUnits.GetValueOrDefault(TimeUnit.Unknown)));
				}
				return string.Format("{0}:{{{1}}}", string.Join(",", options));
			}
		}
	}
}
