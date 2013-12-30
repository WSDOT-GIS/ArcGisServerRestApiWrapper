using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// Parameters to pass to the Solve REST endpoint of the Route service. <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>
    /// </summary>
    public class SolveParameters
    {
        /// <summary>
        /// <para>Use this parameter to specify two or more locations between which the route is to be found.</para>
        /// <para>Use this (instead of <see cref="SolveParameters.stopsAsFeatures"/> or <see cref="SolveParameters.stopsAsUrl"/>)
        /// if you need to specify only stop point geometries in the default spatial reference WGS84 such as 
        /// the longitude and latitude values.</para>
        /// <para>
        /// One of the following values must be specified: <see cref="SolveParameters.stops"/>, 
        /// <see cref="SolveParameters.stopsAsFeatures"/>, 
        /// <see cref="SolveParameters.stopsAsUrl"/>.
        /// </para>
        /// </summary>
        public IEnumerable<double[]> stops { get; set; }
        /// <summary>
        /// <para>Use this parameter to specify two or more locations between which the route is to be found.</para>
        /// <para>
        /// One of the following values must be specified: <see cref="SolveParameters.stops"/>, 
        /// <see cref="SolveParameters.stopsAsFeatures"/>, 
        /// <see cref="SolveParameters.stopsAsUrl"/>.
        /// </para>
        /// </summary>
        public FeatureSet stopsAsFeatures { get; set; }
        /// <summary>
        /// Use this parameter to specify two or more locations between which the route is to be found.
        /// <para>Specify a REST query request to any ArcGIS for Server feature, map, or geoprocessing service that returns a JSON feature set.</para>
        /// <para>One of the following values must be specified: <see cref="SolveParameters.stops"/>, 
        /// <see cref="SolveParameters.stopsAsFeatures"/>, 
        /// <see cref="SolveParameters.stopsAsUrl"/>.
        /// </para>
        /// </summary>
        public string stopsAsUrl { get; set; }

        /// <summary>
        /// Start time.
        /// </summary>
        public DateTime? startTime { get; set; }
        /// <summary>Indicates if startTime is UTC.</summary>
        public bool? startTimeIsUTC { get; set; }
        /// <summary>
        /// <para>Use this parameter to specify if the service should reorder stops to find the optimized route. 
        /// If the parameter value is false, the service returns a route that visits stops in the order you define. 
        /// If the parameter value is true, the service finds the best order to visit the stops. 
        /// The service will account for a variety of variables so that the total travel distance or travel time for the route is minimized. 
        /// You can elect to preserve the origin and/or the destination stops while allowing the service to reorder intermediary stops by setting 
        /// preserveFirstStop and preserveLastStop parameters. The default value for this parameter is false.</para>
        /// <para>Caution: A true parameter value causes the service to switch from solving a shortest-path 
        /// problem to a traveling salesperson problem (TSP). Solving a TSP is computer-intensive operation and incurs 
        /// additional service credits per route.
        /// </summary>
        public bool? findBestSequence { get; set; }
        /// <summary>
        /// Use this parameter to indicate whether the service should keep the first stop fixed when reordering the stops. The possible parameter values are true, or false. The default value is true. This parameter is applicable only if findBestSequence parameter value is true.
        /// </summary>
        public bool? preserveFirstStop { get; set; }
        /// <summary>
        /// Use this parameter to indicate whether the service should keep the last stop fixed when reordering the stops. The possible parameter values are true, or false. The default value is true. This parameter is applicable only if findBestSequence parameter value is true.
        /// </summary>
        public bool? preserveLastStop { get; set; }
        /// <summary>
        /// Use this parameter to indicate if the service should consider time windows specified on the stops when finding the best route. The possible parameter values are true, or false. The time windows are specified on stops using the TimeWindowStart and TimeWindowEnd attributes. 
        /// </summary>
        public bool? useTimeWindows { get; set; }
        /// <summary>
        /// Use this parameter to restrict or permit the route from making U-turns at junctions. In order to understand the parameter values, keep in mind that a junction is a point where only two streets intersect each other. If three or more streets intersect at a point, it is called as an intersection. The end of a culs-de-sac is called as the dead-end.
        /// </summary>
        public esriNFSB? restrictUTurns { get; set; }
        /// <summary>
        /// <para>Use this parameter to specify if hierarchy should be used when finding the route. Using a hierarchy results in the service preferring higher-order streets such as freeways to lower-order streets such as local roads. A parameter value of true indicates that the service should use hierarchy and can be used to simulate the driver preference of traveling on freeways instead of local roads even if that means a longer trip. This is especially true when finding long distance routes, as the driver will usually prefer to travel on freeways as much as possible and avoid the local roads. Using hierarchy is computationally faster, especially for long distance routes, as the service has to select the best route from a relatively smaller subset of streets. A parameter value of false indicates that the service should consider all the streets and not prefer higher-order streets when finding the route. This is often used when finding short-distance routes like routes within a city.</para>
        /// <para>Caution: The service automatically reverts to using hierarchy if the straight-line distance between the first two stops is greater than 50 miles (80.46 kilometers) even if you have specified to find the route without using hierarchy.</para>
        /// </summary>
        public bool? useHierarchy { get; set; }
        
        /// <summary>
        /// Use this parameter to specify if you want to find the quickest route that minimizes the travel time or the shortest route that minimizes the travel distance between the stops. The parameter can have the following values:
        /// <list type="table">
        /// <item><term>TravelTime</term><description>Specifies that the travel time between the stops should be minimized. The total travel time between the stops is calculated in minutes. This is the default value.</description></item>
        /// <item><term>Miles</term><description>Specifies that the travel distance between the stops should be minimized. The total distance between the stops is calculated in miles.</description></item>
        /// <item><term>Kilometers</term><description>Specifies that the travel distance between the stops should be minimized. The total distance between the stops is calculated in kilometers.</description></item>
        /// </list>
        /// </summary>
        public string impedanceAttributeName { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify if the service should accumulate values other than the value specified as the impedanceAttributeName when finding the best route. For example, if your impedanceAttributeName is set to TravelTime, the total travel time for the route will be calculated by the service. However, if you also want to calculate the total distance of the route in miles, you can specify Miles as the value for the accumulateAttributeNames parameter.</para>
        /// <para>The parameter value should be specified as a comma separated list of names. The possible parameter values are same as the impedanceAttributeName parameter. For example, accumulateAttributeNames=Miles,Kilometers indicates that the total cost of the route should also be calculated in miles and kilometers. This is also the default value for this parameter.</para>
        /// <para>Note: The values specified for the accumulateAttributeNames parameter are purely for reference. The service only uses the value specified for the impedanceAttributeName parameter to find the best route.</para>
        /// </summary>
        public IEnumerable<string> accumulateAttributeNames { get; set; }


        /// <summary>
        /// <para>Use this parameter to specify which restrictions should be honored by the service when finding the best route. 
        /// A restriction represents a driving preference or requirement. In most cases, restrictions cause roads to be prohibited, 
        /// but they can also cause them to be avoided or preferred. For instance, using an Avoid Toll Roads restriction will result 
        /// in a route that will include toll roads only when it is absolutely required to travel on toll roads in order to visit a stop.
        /// Height Restriction makes it possible to route around any clearances that are lower than the height of your vehicle. 
        /// If you are carrying corrosive materials on your vehicle, using the Any Hazmat Prohibited restriction prevents hauling the
        /// materials along roads where it is marked as illegal to do so.</para>
        /// <para>The parameter value is specified as list of restriction names. 
        /// For example, the default value for this parameter is 
        /// restritionAttributeNames=Avoid Carpool Roads, Avoid Express Lanes, Avoid Gates, Avoid Private Roads, Avoid Unpaved Roads, Driving an Automobile, Roads Under Construction Prohibited, Through Traffic Prohibited. 
        /// A value of none indicates that no restrictions should be used when finding the best route. 
        /// A list of possible values can be found at <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>.
        /// </para>
        /// </summary>
        public IEnumerable<string> restrictionAttributeNames { get; set; }

        /// <summary><see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public IEnumerable<AttributeParameterValue> attributeParameterValues { get; set; }

        /// <summary><see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public IEnumerable<double[]> barriers { get; set; }
        /// <summary><see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public FeatureSet barriersAsPointFeatures { get; set; }
        /// <summary><see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public FeatureSet polylineBarriers { get; set; }
        /// <summary><see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public FeatureSet polygonBarriers { get; set; }


        /// <summary>Use this parameter to specify if the service should generate driving directions between the stops. The possible values for this parameter are true, or false. A true value indicates that the directions will be generated and configured based on the values for the directionsLanguage, directionsOutputType, directionsStyleName, and directionsLengthUnits parameters. The directions are available in the directions property of the JSON response. The default value for the returnDirections parameter is true.</summary>
        public bool? returnDirections { get; set; }
        /// <summary>Use this parameter to specify the language that should be used when generating driving directions. This parameter is used only when the returnDirections parameter is set to true.
        /// <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/></summary>
        public string directionsLanguage { get; set; }

        /// <summary>
        /// Use this parameter to define the content and verbosity of the driving directions. This parameter is used only when the returnDirections parameter is set to true. The default value is esriDOTStandard.
        /// </summary>
        public DirectionsOutputType? directionsOutputType { get; set; }
        /// <summary>
        /// Use this parameter to specify the name of the formatting style for the directions. The default value is NA Desktop.
        /// This parameter is used only when the returnDirections parameter is set to true. The parameter can be specified using the following values:
        /// <list type="table">
        /// <item><term>NA Desktop</term><description>Generates turn-by-turn directions suitable for printing.</description></item>
        /// <item><term>NA Navigation</term><description>Generates turn-by-turn directions designed for an in-vehicle navigation device.</description></item>
        /// </list>
        /// </summary>
        public string directionsStyleName { get; set; }

        /// <summary>
        /// Use this parameter to specify the units for displaying travel distance in the driving directions. This parameter is used only when the returnDirections parameter is set to true. The parameter can be specified using one of the values: esriNAUCentimeters, esriNAUDecimalDegrees, esriNAUDecimeters, esriNAUFeet, esriNAUInches, esriNAUKilometers, esriNAUMeters, esriNAUMiles, esriNAUMillimeters, esriNAUNauticalMiles, esriNAUPoints, esriNAUYards. The default value is esriNAUKilometers.
        /// </summary>
        public NetworkAnalystUnits? directionsLengthUnits { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify if the service should return routes. The possible values for this parameter are true, or false. 
        /// A true value indicates that the routes will be generated and the shape of the routes depends on the value for the outputLines parameter. 
        /// The routes are available in the routes property of the JSON response. The default value for the returnRoutes parameter is true.</para>
        /// <para>Tip: There are certain cases when it is not desirable to use a value of true for returnRoutes parameter. 
        /// For example, if your application has to display only the driving directions between the stops, 
        /// it is sufficient to just set the returnDirections parameter to true to get the desired result. 
        /// Setting the returnRoutes parameter to true will not provide any additional information and will increase the overall response size 
        /// which may not be suitable for certain applications such as those running on mobile devices with low bandwidth data connections.</para>
        /// </summary>
        public bool? returnRoutes { get; set; }

        /// <summary>
        /// Use this parameter to specify the type of route features that are output by the service. This parameter is applicable only if the returnRoutes parameter is set to true.
        /// <see href="http://resources.arcgis.com/en/help/arcgis-rest-api/index.html#/Route_service/02r300000036000000/"/>
        /// </summary>
        public OutputLineOptions? outputLines { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify if stops will be returned by the service. The possible values for this parameter are true, or false. A true value indicates that the stops used as input will be returned as part of the stops property in the JSON response. The default value for this parameter is false.</para>
        /// <para>When finding optimized routes, the returnStops property can be set to true to determine the optimal sequence in which the route visits a particular stop by checking the Sequence property in the JSON response. If you have specified the stops parameter using a REST query request to any ArcGIS for Server feature, map, or geoprocessing service that returns a JSON feature set, returning stops can allow you to draw the stop locations in your application. You may also want to set the returnStops property to true in order to determine if the stops were successfully located on the street network or had some other errors by checking the Status property in the JSON response. <para></para>
        /// </summary>
        public bool? returnStops { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify if barriers will be returned by the service. The possible values for this parameter are true, or false. This parameter is applicable only if you have specified the value for the barriers parameter. A true value indicates that the point barriers used as input will be returned as part of the barriers property in the JSON response. The default value for this parameter is false.</para>
        /// <para>If you have specified the barriers parameter using a REST query request to any ArcGIS for Server feature, map, or geoprocessing service that returns a JSON feature set, returning barriers can allow you to draw the point barrier locations in your application. You may also want to set the returnBarriers property to true in order to determine if the point barriers were successfully located on the street network or had some other errors by checking the Status property in the JSON response.</para>
        /// </summary>
        public bool? returnBarriers { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify if polyline barriers will be returned by the service. The possible values for this parameter are true, or false. This parameter is applicable only if you have specified the value for the polylineBarriers parameter. A true value indicates that the polyline barriers used as input will be returned as part of the polylineBarriers property in the JSON response. The default value for this parameter is false.</para>
        /// <para>If you have specified the polylineBarriers parameter using a REST query request to any ArcGIS for Server feature, map, or geoprocessing service that returns a JSON feature set, the returnPolylineBarriers parameter can be set to true so that you can draw the polyline barrier locations in your application.</para>
        /// </summary>
        public bool? returnPolylineBarriers { get; set; }


        /// <summary>
        /// <para>Use this parameter to specify if polygon barriers will be returned by the service. The possible values for this parameter are true, or false. This parameter is applicable only if you have specified the value for the polygonBarriers parameter. A true value indicates that the polygon barriers used as input will be returned as part of the polygonBarriers property in the JSON response. The default value for this parameter is false.</para>
        /// <para>If you have specified the polygonBarriers parameter using a REST query request to any ArcGIS for Server feature, map, or geoprocessing service that returns a JSON feature set, the returnPolygonBarriers parameter can be set to true so that you can draw the polygon barrier locations in your application. </para>
        /// </summary>
        public bool? returnPolygonBarriers { get; set; }

        /// <summary>Use this parameter to specify if invalid stops should be ignored when finding the best route. The possible values for this parameter are true, or false. A stop is considered as invalid by the service if there are no streets within 12.42 miles (20 kilometers) of the stop location. If this parameter is set to false any invalid stop in your request will cause the service to return a failure. The default value for this parameter is true. </summary>
        public bool? ignoreInvalidLocations { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify the spatial reference of the geometries such as the routes or the stops returned by the service. The parameter value can be specified as a well-known ID (WKID) for the spatial reference. . If outSR is not specified, the geometries are returned in the default spatial reference WGS84. You can find the WKID for your spatial reference depending on whether the coordinates should be represented in a geographic coordinate system or a projected coordinate system.</para>
        /// <para>Many of the basemaps provided by ArcGIS Online are in the Web Mercator spatial reference (WKID 102100). Specifying outSR=102100 will return the geometries for the routes in the Web Mercator spatial reference and can be used to draw the routes on top of the basemaps.</para>
        /// </summary>
        public int? outSR { get; set; }

        /// <summary>
        /// <para>Use this parameter to specify by how much you want to simplify the route geometry returned by the service. Simplification maintains critical points on a route, such as turns at intersections, to define the essential shape of the route and removes other points. The simplification distance you specify is the maximum allowable offset that the simplified line can deviate from the original line. Simplifying a line reduces the number of vertices that are part of the route geometry. This reduces the overall response size and also improves the performance for drawing the route shapes in the applications.</para>
        /// <para>The default value for this parameter is 10. The units are specified with the outputGeometryPrecisionUnits parameter.</para>
        /// </summary>
        public float? outputGeometryPrecision { get; set; }

        /// <summary>
        /// Use this parameter to specify the units for the value specified for the outputGeometryPrecision parameter. The parameter value should be specified as one of the following values: esriCentimeters, esriDecimalDegrees, esriDecimeters, esriFeet, esriInches, esriKilometers, esriMeters, esriMiles, esriMillimeters, esriNauticalMiles, esriPoints, and esriYards. The default value is esriMeters. 
        /// </summary>
        public MeasureUnits? outputGeometryPrecisionUnits { get; set; }

        /// <summary>
        /// Converts this object into a query string for use with the REST endpoint.
        /// </summary>
        /// <returns></returns>
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
