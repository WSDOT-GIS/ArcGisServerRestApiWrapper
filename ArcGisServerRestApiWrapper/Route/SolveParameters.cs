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
        // TODO: Figure out bettery types for IEnumerable<object> properties.

        public IEnumerable<object> stops { get; set; }
        public DateTime? startTime { get; set; }
        public bool? startTimeIsUTC { get; set; }
        public bool? findBestSequence { get; set; }
        public bool? preserveFirstStop { get; set; }
        public bool? preserveLastStop { get; set; }
        public bool? useTimeWindows { get; set; }
        public esriNFSB? restrictUTurns { get; set; }
        public bool? useHierarchy { get; set; }
        public string impedanceAttributeName { get; set; } // Valid values are TravelTime, Miles, Kilometers.
        public IEnumerable<string> accumulateAttributeNames { get; set; }
        public IEnumerable<string> restrictionAttributeNames { get; set; }
        public IEnumerable<AttributeParameterValue> attributeParameterValues { get; set; }
        public IEnumerable<object> barriers { get; set; }
        public IEnumerable<object> polylineBarriers { get; set; }
        public IEnumerable<object> polygonBarriers { get; set; }
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

            if (this.stops != null && stops.Count() > 1) {
            }
            else
            {
                throw new ArgumentException();
            }

            if (this.startTime != null) { }
            if (this.startTimeIsUTC != null) { }
            if (this.findBestSequence != null) { }
            if (this.preserveFirstStop != null) { }
            if (this.preserveLastStop != null) { }
            if (this.useTimeWindows != null) { }
            if (this.restrictUTurns != null) { }
            if (this.useHierarchy != null) { }
            if (this.impedanceAttributeName != null) { } // Valid values are TravelTime, Miles, Kilometers.
            if (this.accumulateAttributeNames != null) { }
            if (this.restrictionAttributeNames != null) { }
            if (this.attributeParameterValues != null) { }
            if (this.barriers != null) { }
            if (this.polylineBarriers != null) { }
            if (this.polygonBarriers != null) { }
            if (this.returnDirections != null) { }
            if (this.directionsLanguage != null) { }
            if (this.directionsOutputType != null) { }
            if (this.directionsStyleName != null) { }
            if (this.directionsLengthUnits != null) { }
            if (this.returnRoutes != null) { }
            if (this.outputLines != null) { }
            if (this.returnStops != null) { }
            if (this.returnBarriers != null) { }
            if (this.returnPolylineBarriers != null) { }
            if (this.returnPolygonBarriers != null) { }
            if (this.ignoreInvalidLocations != null) { }
            if (this.outSR != null) { }
            if (this.outputGeometryPrecision != null) { }
            if (this.outputGeometryPrecisionUnits != null) { }

            throw new NotImplementedException();
        }
    }
}
