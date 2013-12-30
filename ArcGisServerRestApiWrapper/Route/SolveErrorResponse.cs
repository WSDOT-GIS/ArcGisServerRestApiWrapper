
namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// The response a <see cref="RouteService.Solve"/> opertaion will return when it fails.
    /// </summary>
    public class SolveErrorResponse
    {
        /// <summary>
        /// Information about the error.
        /// </summary>
        public SolveErrorInfo error { get; set; }
    }

    /// <summary>
    /// The information about a <see cref="SolveErrorResponse"/>.
    /// </summary>
    public class SolveErrorInfo
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
