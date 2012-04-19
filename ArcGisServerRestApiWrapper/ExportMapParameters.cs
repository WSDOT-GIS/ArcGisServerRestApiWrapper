using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcGisServerRestApiWrapper
{
	public class ExportMapParameters
	{
		/// <summary>
		/// f
		/// </summary>
		public ExportMapResponseFormat? ResponseFormat { get; set; }

		/// <summary>
		/// bbox
		/// </summary>
		public double[] BoundingBox { get; set; }

		/// <summary>
		/// size
		/// </summary>
		public int[] Size { get; set; }

		/// <summary>
		/// dpi
		/// </summary>
		public int? Dpi { get; set; }

		/// <summary>
		/// imageSR
		/// </summary>
		public object ImageSR { get; set; } // either an int (WKID) or spatial reference object

		/// <summary>
		/// bboxSR
		/// </summary>
		public object BoundingBoxSR { get; set; }  // either an int (WKID) or spatial reference object

		/// <summary>
		/// format
		/// </summary>
		public ExportMapImageFormat? ImageFormat { get; set; }

		/// <summary>
		/// layerDefs
		/// </summary>
		public LayerDefinition[] LayerDefinitions { get; set; }

		/// <summary>
		/// layers
		/// </summary>
		public ExportMapLayersMode? LayersMode { get; set; }

		/// <summary>
		/// layers
		/// </summary>
		public int[] Layers { get; set; }

		/// <summary>
		/// Transparent
		/// </summary>
		public bool? Transparent { get; set; }

		/// <summary>
		/// time
		/// </summary>
		public DateTime? Time { get; set; }

		/// <summary>
		/// time
		/// </summary>
		public DateTime? EndTime { get; set; }

		/// <summary>
		/// layerTimeOptions
		/// </summary>
		public ExportMapLayerTimeOptions[] LayerTimeOptions { get; set; }

		/// <summary>
		/// Returns a query string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			if (this.ResponseFormat.HasValue)
			{
				builder.AppendFormat("f={0}", this.ResponseFormat.Value.ToString().ToLower());
			}
			if (this.BoundingBox != null && this.BoundingBox.Length >= 4)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("bbox={0},{1},{2},{3}", this.BoundingBox[0], this.BoundingBox[1], this.BoundingBox[2], this.BoundingBox[3]);
				if (this.BoundingBoxSR != null)
				{
					if (builder.Length > 0) builder.Append("&");
					builder.AppendFormat("bboxSR={0}", this.BoundingBoxSR);
				}

			}
			if (this.Size != null && this.Size.Length >= 2)
			{

				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("size={0},{1}", this.Size[0], this.Size[1]);
			}

			if (this.Dpi.HasValue)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("dpi={0}", Dpi);
			}

			if (this.ImageSR != null)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("imageSR={0}", ImageSR);
			}

			if (this.ImageFormat.HasValue)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("format={0}", ImageFormat.Value.ToString().ToLower());
			}

			if (this.LayerDefinitions != null)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("layerDefs={0}", string.Join<LayerDefinition>(";", this.LayerDefinitions));
			}

			if (this.Layers != null && this.Layers.Length > 0 && this.LayersMode.HasValue)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("layers={0}:{1}", this.LayersMode.Value.ToString().ToLower(), string.Join(",", this.Layers));
			}

			if (this.Transparent.HasValue)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("transparent={0}", this.Transparent);
			}

			if (this.Time.HasValue)
			{
				if (builder.Length > 0) builder.Append("&");
				if (this.EndTime.HasValue)
				{
					// timeRange
					builder.AppendFormat("time={0},{1}", this.Time.Value.ToJavaScriptTicks(), this.EndTime.Value.ToJavaScriptTicks());
				}
				else
				{
					// timeInstant
					builder.AppendFormat("time={0}", this.Time.Value.ToJavaScriptTicks());
				}
			}

			if (this.LayerTimeOptions != null && this.LayerTimeOptions.Length < 0)
			{
				if (builder.Length > 0) builder.Append("&");
				builder.AppendFormat("layerTimeOption={0}", '{', string.Join<ExportMapLayerTimeOptions>(",", this.LayerTimeOptions), '}');
			}

			return builder.ToString();
		}
	}

	public class LayerDefinition
	{
		public int LayerId { get; set; }
		public string Query { get; set; }

		public override string ToString()
		{
			return string.Format("{0}:{1}", LayerId, Query);
		}
	}

	public enum ExportMapResponseFormat
	{
		Html,
		Json,
		Image,
		Kmz
	}

	public enum ExportMapImageFormat
	{
		Png,
		Png8,
		Png24,
		Png32,
		Jpg,
		Pdf,
		Bmp,
		Gif,
		Svg
	}

	public enum ExportMapLayersMode
	{
		Show,
		Hide,
		Include,
		Exclude
	}

	public class ExportMapLayerTimeOptions
	{
		public int LayerId { get; set; }
		public bool UseTime { get; set; }
		public bool? TimeDataCumulative { get; set; }

		public double? TimeOffset { get; set; }
		public TimeUnit? TimeOffsetUnits { get; set; }

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
				return string.Concat("{", string.Join(",", options), "}");
			}
		}
	}

	public enum TimeUnit
	{
		Unknown,
		Centuries, 
		Days, 
		Decades,
		Hours, 
		Milliseconds, 
		Minutes,
		Months, 
		Seconds, 
		Weeks, 
		Years,
	}
}
