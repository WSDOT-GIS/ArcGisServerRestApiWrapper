using System;

namespace Esri.ArcGisServer.Rest.Authentication
{
    /// <summary>
    /// Represents a token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The token string that will be passed to request URLs.
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// The date and time that the token will expire.
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// Creates a new <see cref="Token"/> without specifying values for properties.
        /// </summary>
        public Token()
        {

        }

        /// <summary>
        /// Creates a new token and initializes its properties.
        /// </summary>
        /// <param name="tokenResponse"></param>
        public Token(TokenResponse tokenResponse)
        {
            ExpirationTime = DateTime.Now.AddSeconds(tokenResponse.expires_in);
            AccessToken = tokenResponse.access_token;
        }

        /// <summary>
        /// Determines if a token as expired. Returns <see langword="true"/> if it has not expired, <see langword="false"/> otherwise.
        /// </summary>
        public bool IsValid
        {
            get {
                return ExpirationTime.HasValue && DateTime.Now < ExpirationTime.Value;
            }
        }

    }

    /// <summary>
    /// Represents the response from the token service.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>The token string that will be passed to request URLs.</summary>
        public string access_token { get; set; }
        /// <summary>The number of seconds from the tokens creation until it expires.</summary>
        public int expires_in { get; set; }
    }
}
