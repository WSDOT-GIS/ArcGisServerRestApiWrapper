
namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// The response a <see cref="GeocodeService.FindAddressCandidates"/> operation will return when it fails.
    /// </summary>
    public class GeocodeErrorResponse
    {
        /// <summary>
        /// Information about the error.
        /// </summary>
        public GeocodeErrorInfo error { get; set; }
    }

    /// <summary>
    /// The information about a <see cref="GeocodeErrorResponse"/>.
    /// </summary>
    public class GeocodeErrorInfo
    {
        /// <summary>
        /// HTTP error code.
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// Basic error message.
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Provides detailed information about the error.
        /// </summary>
        public string[] details { get; set; }
    }
}
