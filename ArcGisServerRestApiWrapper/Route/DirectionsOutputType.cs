namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// Values for use with <see cref="SolveParameters.directionsOutputType"/> indicating the level of detail of returned directions.
    /// </summary>
    public enum DirectionsOutputType
    {
        /// <summary>Complete</summary>
        esriDOTComplete,
        /// <summary>Complete, No events.</summary>
        esriDOTCompleteNoEvents,
        /// <summary>Instructions only.</summary>
        esriDOTInstructionsOnly,
        /// <summary>Standard. (This is the default.)</summary>
        esriDOTStandard,
        /// <summary>Summary only.</summary>
        esriDOTSummaryOnly,
    }
}
