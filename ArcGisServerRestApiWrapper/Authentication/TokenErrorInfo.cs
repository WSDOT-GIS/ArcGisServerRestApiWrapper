
namespace Esri.ArcGisServer.Rest.Authentication
{
    public class TokenErrorInfo
    {
        public int code { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string message { get; set; }
        public string[] details { get; set; }
    }
}
