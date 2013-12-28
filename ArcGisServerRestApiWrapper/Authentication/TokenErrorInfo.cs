
namespace Esri.ArcGisServer.Rest.Authentication
{
    /// <summary>
    /// Error information returned from a failed attempt to retrieve a token.
    /// </summary>
    public class TokenErrorInfo
    {
        /// <summary>Error code</summary>
        public int code { get; set; }
        /// <summary>Error</summary>
        public string error { get; set; }
        /// <summary>A description of the error.</summary>
        public string error_description { get; set; }
        /// <summary>A message associated with the error.</summary>
        public string message { get; set; }
        /// <summary>Details about what caused the error.</summary>
        public string[] details { get; set; }
    }
}
