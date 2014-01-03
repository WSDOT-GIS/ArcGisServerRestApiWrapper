using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// Parameters for use with the geocode find operation.
    /// </summary>
    public class FindParameters
    {
        /// <summary>
        /// Specifies the location to be geocoded. This can be a street address, place name, postal code, or POI. It is a required parameter. 
        /// </summary>
        /// <example>Street address <c>380 New York St, Redlands, California 92373</c></example>
        public string text { get; set; }
        
        /// <summary>
        /// <para>A value representing the country. Providing this value increases geocoding speed.</para>
        /// <para>Acceptable values include the full country name, the ISO 3166-1 2-digit country code, or the ISO 3166-1 3-digit country code.</para>
        /// <para>A list of supported countries and codes is available <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/02r3/02r300000018000000.htm"/>here</para>.
        /// </summary>
        /// <example>USA</example>
        public string sourceCountry { get; set; }

        /// <summary>
        /// A set of bounding box coordinates that limit the search area to a specific region. 
        /// This is especially useful for applications in which a user will search for places and addresses only within the current map extent. 
        /// </summary>
        /// <remarks>Assumed to be in the same spatial reference system as the geocode service.</remarks>
        public double[] bbox { get; set; }

        /// <summary>
        /// Use this parameter instead of <see cref="FindParameters.bbox"/> if you need to specify a bounding box in a different coordinate system
        /// than the one used by the geocode service.
        /// A set of bounding box coordinates that limit the search area to a specific region. 
        /// This is especially useful for applications in which a user will search for places and addresses only within the current map extent. 
        /// </summary>
        public Geometry bboxAsGeometry { get; set; }
        
        /// <summary>
        /// <para>Defines an origin point location that is used with the distance parameter to sort geocoding candidates based upon their proximity to the location. The distance parameter specifies the radial distance from the location in meters. The priority of candidates within this radius is boosted relative to those outside the radius.</para>
        /// <para>This is useful in mobile applications where a user will want to search for places in the vicinity of their current GPS location; the location and distance parameters can be used in this scenario.</para>
        /// <para>The location parameter can be specified without specifying a distance. If distance is not specified, it defaults to 2000 meters.</para>
        /// </summary>
        /// <remarks>Assumed to be in the same spatial reference system as the geocode service.</remarks>
        public double[] location { get; set; }

        /// <summary>
        /// <para>Defines an origin point location that is used with the distance parameter to sort geocoding candidates based upon their proximity to the location. The distance parameter specifies the radial distance from the location in meters. The priority of candidates within this radius is boosted relative to those outside the radius.</para>
        /// <para>This is useful in mobile applications where a user will want to search for places in the vicinity of their current GPS location; the location and distance parameters can be used in this scenario.</para>
        /// <para>The location parameter can be specified without specifying a distance. If distance is not specified, it defaults to 2000 meters.</para>
        /// </summary>
        public Geometry locationAsGeometry { get; set; }

        /// <summary>
        /// <para>Specifies the radius of an area around a point location which is used to boost the rank of geocoding candidates so that candidates closest to the location are returned first. The distance value is in meters.</para>
        /// <para>If the distance parameter is specified, then the location parameter must be specified as well.</para>
        /// <para>It is important to note that unlike the bbox parameter, the location/distance parameters allow searches to extend beyond the specified search radius. They are not used to filter results, but rather to rank resulting candidates based on their distance from a location. You must pass a bbox value in addition to location/distance if you want to confine the search results to a specific area.</para>
        /// </summary>
        public double? distance { get; set; }
        
        /// <summary>
        /// The spatial reference of the x/y coordinates returned by a geocode request. This is useful for applications using a map with a spatial reference different than that of the geocode service. 
        /// </summary>
        public int? outSR { get; set; }
        /// <summary>
        /// The spatial reference of the x/y coordinates returned by a geocode request. This is useful for applications using a map with a spatial reference different than that of the geocode service. 
        /// </summary>
        public SpatialReference outSRAsObject { get; set; }

        /// <summary>
        /// <para>The list of fields to be returned in the response.
        /// Descriptions for each of these fields are available in the 
        /// <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/02r3/02r300000017000000.htm#ESRI_SECTION1_42D7D3D0231241E9B656C01438209440">
        /// Output fields section of the geocode service documentation</see></para>. 
        /// <para>The returned address, x/y coordinates of the match location, match score, spatial reference, extent of the output feature, and the addr_type (match level) are returned by default. </para>
        /// <para>Use a single value of "*" to specify that all fields are to be returned.</para>
        /// </summary>
        public IEnumerable<string> outFields { get; set; }

        /// <summary>
        /// <para>The maximum number of locations to be returned by a search, up to the maximum number allowed by the service. If not specified, then one location will be returned.</para>
        /// <para>The world geocoding service allows up to 20 candidates to be returned for a single request. Note that up to 50 POI candidates can be returned.</para>
        /// </summary>
        public int? maxLocations { get; set; }

        /// <summary>
        /// <para>Allows the results of single geocode transactions to be persisted.</para>
        /// <para>Applications are contractually prohibited from storing the results of single geocode transactions. 
        /// This restriction applies to the Find, findAddressCandidates, and reverseGeocode methods.</para>
        /// <para>However, by passing the forStorage parameter with value "true" in a geocode request, a client 
        /// application is allowed to store the results. An ArcGIS Online organization subscription is required 
        /// to use this parameter, and an access token must be passed with the request. 
        /// Instructions for composing a request with authentication are provided in the 
        /// <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/02r3/02r3000000n3000000.htm">Batch geocode authentication</see> topic.</para>
        /// <para>ArcGIS Online service credits are deducted from the organization account for each geocode transaction 
        /// which includes the parameter. 
        /// Refer to the <see href="http://www.esri.com/software/arcgis/arcgisonline/credits">ArcGIS Online service credits overview page</see> 
        /// for more information about how credits are charged.</para>
        /// <para>The default value for the forStorage parameter is "false".</para>
        /// </summary>
        public bool? forStorage { get; set; }

        
        /// <summary>
        /// Converts the object into a dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            var output = new Dictionary<string, object>();
            output[text] = this.text;
            output.AddFirstNonNullValue("bbox", this.bbox, this.bboxAsGeometry);
            output.AddFirstNonNullValue("location", this.location, this.locationAsGeometry);
            if (this.distance.HasValue)
            {
                output.Add("distance", this.distance);
            }
            output.AddFirstNonNullValue("outSR", this.outSR, this.outSRAsObject);
            if (this.outFields != null && this.outFields.Count() > 0)
            {
                output.Add("outFields", string.Join(",", this.outFields));
            }
            if (this.maxLocations.HasValue) output.Add("maxLocations", this.maxLocations);
            if (this.forStorage.HasValue) output.Add("forStorage", this.forStorage);
            output.Add("f", "json");

            return output;
        }

    }
}
