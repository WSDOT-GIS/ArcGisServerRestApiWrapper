using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    // See http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/

    /// <summary>
    /// 
    /// </summary>
    public class SolveParameters
    {
        public IEnumerable<double[]> stops { get; set; }
        public FeatureSet stopsAsFeatures { get; set; }
        public string stopsAsUrl { get; set; }

        public DateTime? startTime { get; set; }
        public bool? startTimeIsUTC { get; set; }
        public bool? findBestSequence { get; set; }
        public bool? preserveFirstStop { get; set; }
        public bool? preserveLastStop { get; set; }
        public bool? useTimeWindows { get; set; }
        public esriNFSB? restrictUTurns { get; set; }
        public bool? useHierarchy { get; set; }
        /// <summary>
        /// If provided, valid value names are: TravelTime, Miles, Kilometers.
        /// </summary>
        public string impedanceAttributeName { get; set; }
        public IEnumerable<string> accumulateAttributeNames { get; set; }
        public IEnumerable<string> restrictionAttributeNames { get; set; }
        public IEnumerable<AttributeParameterValue> attributeParameterValues { get; set; }

        public IEnumerable<double[]> barriers { get; set; }
        public FeatureSet barriersAsPointFeatures { get; set; }
        public FeatureSet polylineBarriers { get; set; }
        public FeatureSet polygonBarriers { get; set; }


        public bool? returnDirections { get; set; }
        public string directionsLanguage { get; set; }
        public DirectionsOutputType? directionsOutputType { get; set; }
        /// <summary>
        /// Valid values are "NA Desktop" or "NA Navigation"
        /// </summary>
        public string directionsStyleName { get; set; }
        public NetworkAnalystUnits? directionsLengthUnits { get; set; }
        public bool? returnRoutes { get; set; }
        public OutputLineOptions? outputLines { get; set; }
        public bool? returnStops { get; set; }
        public bool? returnBarriers { get; set; }
        public bool? returnPolylineBarriers { get; set; }
        public bool? returnPolygonBarriers { get; set; }
        public bool? ignoreInvalidLocations { get; set; }
        public int? outSR { get; set; }
        public float? outputGeometryPrecision { get; set; }
        public MeasureUnits? outputGeometryPrecisionUnits { get; set; }

        public string ToQueryString()
        {
            var sBuilder = new StringBuilder();

            if (this.stops != null && stops.Count() > 1)
            {
                sBuilder.AppendFormat("stops={0}", this.stops.ToListString());
            }
            else if (this.stopsAsFeatures != null && stops.Count() > 1)
            {
                sBuilder.AppendFormat("stops={0}", JsonSerializer.SerializeToString(this.stopsAsFeatures));
            }
            else if (!string.IsNullOrWhiteSpace(this.stopsAsUrl))
            {
                sBuilder.AppendFormat("stops={0}", this.stopsAsUrl);
            }

            if (this.startTime.HasValue)
            {
                sBuilder.AppendFormat("&startTime=\"timeOfDay\":{0}", this.startTime.Value.ToJavaScriptTicks());
            }
            if (this.startTimeIsUTC.HasValue)
            {
                sBuilder.AppendFormat("&startTimeIsUTC={0}", this.startTimeIsUTC.Value);
            }
            if (this.findBestSequence.HasValue)
            {
                sBuilder.AppendFormat("&findBestSequence={0}", this.findBestSequence.Value);
            }
            if (this.preserveFirstStop.HasValue)
            {
                sBuilder.AppendFormat("&preserveFirstStop={0}", this.preserveFirstStop.Value);
            }
            if (this.preserveLastStop.HasValue)
            {
                sBuilder.AppendFormat("&preserveLastStop={0}", this.preserveLastStop.Value);
            }
            if (this.useTimeWindows.HasValue)
            {
                sBuilder.AppendFormat("&useTimeWindows={0}", this.useTimeWindows.Value);
            }
            if (this.restrictUTurns.HasValue)
            {
                sBuilder.AppendFormat("&restrictUTurns={0}", this.restrictUTurns.Value);
            }
            if (this.useHierarchy.HasValue)
            {
                sBuilder.AppendFormat("&useHierarchy={0}", this.useHierarchy.Value);
            }
            if (!string.IsNullOrWhiteSpace(this.impedanceAttributeName))
            {
                sBuilder.AppendFormat("&impedanceAttributeName={0}", this.impedanceAttributeName);
            }
            if (this.accumulateAttributeNames != null)
            {
                sBuilder.AppendFormat("&accumulateAttributeNames={0}", string.Join(",", this.accumulateAttributeNames));
            }
            if (this.restrictionAttributeNames != null)
            {
                sBuilder.AppendFormat("&restrictionAttributeNames={0}", string.Join(",", this.restrictionAttributeNames));
            }
            if (this.attributeParameterValues != null)
            {
                sBuilder.AppendFormat("&attributeParameterValues={0}", string.Join(",", this.attributeParameterValues));
            }

            if (this.barriers != null)
            {
                sBuilder.AppendFormat("&barriers={0}", this.barriers.ToListString());
            }
            else if (this.barriersAsPointFeatures != null)
            {
                sBuilder.AppendFormat("&barriers={0}", JsonSerializer.SerializeToString(this.barriersAsPointFeatures));
            }

            if (this.polylineBarriers != null)
            {
                sBuilder.AppendFormat("&polylineBarriers={0}", JsonSerializer.SerializeToString(this.polylineBarriers));
            }
            if (this.polygonBarriers != null)
            {
                sBuilder.AppendFormat("&polygonBarriers={0}", JsonSerializer.SerializeToString(this.polygonBarriers));

            }
            if (this.returnDirections.HasValue)
            {
                sBuilder.AppendFormat("&returnDirections={0}", this.returnDirections.Value);
            }
            if (!string.IsNullOrWhiteSpace(this.directionsLanguage))
            {
                sBuilder.AppendFormat("&directionsLanguage={0}", this.directionsLanguage);
            }
            if (this.directionsOutputType.HasValue)
            {
                sBuilder.AppendFormat("&directionsOutputType={0}", Enum.GetName(typeof(DirectionsOutputType), this.directionsOutputType.Value));
            }
            if (!string.IsNullOrWhiteSpace(this.directionsStyleName))
            {
                sBuilder.AppendFormat("&directionsStyleName={0}", this.directionsStyleName);
            }
            if (this.directionsLengthUnits.HasValue)
            {
                sBuilder.AppendFormat("&directionsLengthUnits={0}", Enum.GetName(typeof(NetworkAnalystUnits), this.directionsLengthUnits.Value));
            }
            if (this.returnRoutes.HasValue)
            {
                sBuilder.AppendFormat("&returnRoutes={0}", this.returnRoutes.Value);
            }
            if (this.outputLines.HasValue)
            {
                sBuilder.AppendFormat("&outputLines={0}", this.outputLines.Value);
            }
            if (this.returnStops.HasValue)
            {
                sBuilder.AppendFormat("&returnStops={0}", this.returnStops.Value);
            }
            if (this.returnBarriers.HasValue)
            {
                sBuilder.AppendFormat("&returnBarriers={0}", this.returnBarriers.Value);
            }
            if (this.returnPolylineBarriers.HasValue)
            {
                sBuilder.AppendFormat("&returnPolylineBarriers={0}", this.returnPolylineBarriers.Value);
            }
            if (this.returnPolygonBarriers.HasValue)
            {
                sBuilder.AppendFormat("&returnPolygonBarriers={0}", this.returnPolygonBarriers.Value);
            }
            if (this.ignoreInvalidLocations.HasValue)
            {
                sBuilder.AppendFormat("&ignoreInvalidLocations={0}", this.ignoreInvalidLocations.Value);
            }
            if (this.outSR.HasValue)
            {
                sBuilder.AppendFormat("&outSR={0}", this.outSR.Value);
            }
            if (this.outputGeometryPrecision.HasValue)
            {
                sBuilder.AppendFormat("&outputGeometryPrecision={0}", this.outputGeometryPrecision.Value);
            }
            if (this.outputGeometryPrecisionUnits.HasValue)
            {
                sBuilder.AppendFormat("&outputGeometryPrecisionUnits={0}", this.outputGeometryPrecisionUnits.Value);
            }

            return sBuilder.ToString();
        }
    }
}
