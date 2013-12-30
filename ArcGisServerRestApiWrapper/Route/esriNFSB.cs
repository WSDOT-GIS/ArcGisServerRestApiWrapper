namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// For use with <see cref="SolveParameters.restrictUTurns"/>.
    /// </summary>
    public enum esriNFSB
    {
        /// <summary>
        /// U-turns are permitted everywhere. Allowing U-turns implies that the vehicle can turn around at a junction and double back on the same street. This is the default value.
        /// </summary>
        esriNFSBAllowBacktrack,
        /// <summary>
        /// U-turns are prohibited at junctions where exactly two adjacent streets meet. 
        /// </summary>
        esriNFSBAtDeadEndsAndIntersections,
        /// <summary>
        /// U-turns are prohibited at all junctions and interesections and are permitted only at dead ends. 
        /// </summary>
        esriNFSBAtDeadEndsOnly,
        /// <summary>
        /// U-turns are prohibited at all junctions, intersections and dead-ends. Note that even when this parameter value is chosen, the route can still make U-turns at stops. If you wish to prohibit U-turns even at the stops, you can set the appropriate value (3) for the CurbApproach attribute of the stops.
        /// </summary>
        esriNFSBNoBacktrack
    }
}
