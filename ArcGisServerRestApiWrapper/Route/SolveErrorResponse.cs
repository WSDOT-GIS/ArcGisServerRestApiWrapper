
namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// The response a <see cref="RouteService.Solve"/> opertaion will return when it fails.
    /// </summary>
    public class SolveErrorResponse
    {
        public SolveErrorInfo error { get; set; }
    }

    /// <summary>
    /// The information about a <see cref="SolveErrorResponse"/>.
    /// </summary>
    public class SolveErrorInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public string[] details { get; set; }
    }

}
