using System;

namespace Esri.ArcGisServer.Rest.Authentication
{
    /// <summary>
    /// Represents a token
    /// </summary>
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime? ExpirationTime { get; set; }

        public Token()
        {

        }

        public Token(TokenResponse tokenResponse)
        {
            ExpirationTime = DateTime.Now.AddSeconds(tokenResponse.expires_in);
            AccessToken = tokenResponse.access_token;
        }

        public bool IsValid
        {
            get {
                return ExpirationTime.HasValue && DateTime.Now < ExpirationTime.Value;
            }
        }

    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
