namespace Esri.ArcGisServer.Rest.Route
{
    public enum OutputLineOptions
    {
        ///<summary>Return the exact shape of the resulting route that is based on the underlying streets. This is the default value.</summary>
        esriNAOutputLineTrueShape,
        ///<summary>Return the exact shape of the resulting route that is based on the underlying streets and include route measurements that keep track of the cumulative travel time or travel distance along the route relative to the first stop. When this value is chosen for the outputLines parameter, each point that make up the route shape will include an M value along with X and Y values. The M value, also known as the measure value, indicates the accumulated travel time or travel distance at that point along the route. The M values can be used to determine how far you have traveled from the start of the route or the remaining distance or time left to reach the destination. The M values store travel time if the impedanceAttributeName is set to TravelTime and store the travel distance if the impedanceAttributeName is set to Kilometers or Miles.</summary>
        esriNAOutputLineTrueShapeWithMeasure,
        ///<summary>Return a straight line between the stops.</summary>
        esriNAOutputLineStraight,
        ///<summary>Do not return any shapes for the routes. This value can be useful in cases where you are only interested in determing the total travel time or travel distance of the route. For example, if your application has already calculated the route and after some time your application needs to only calculate the expected time of arrival (ETA) to the destination, you can set the returnRoutes parameter to true and the outputLines parameter to esriNAOutputLineNone. The routes property of the JSON response will only contain the total travel time that can be used to determine the ETA. Since the route shape is not returned when using the esriNAOutputLineNone value, the response size will be considerably smaller.</summary>
        esriNAOutputLineNone,
    }
}
