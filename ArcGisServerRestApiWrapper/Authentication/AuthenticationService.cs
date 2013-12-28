using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Esri.ArcGisServer.Rest.Authentication
{
    /// <summary>
    /// This is used to get tokens.
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// The URL of the service.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// Creates a new instance of the <see cref="AuthenticationService"/>.
        /// </summary>
        /// <param name="url"></param>
        public AuthenticationService(string url="https://www.arcgis.com/sharing/oauth2/token")
        {
            this.Url = url;
        }

        /// <summary>
        /// Gets a token.
        /// </summary>
        /// <param name="clientId">OAuth ClientId</param>
        /// <param name="clientSecret">OAuth ClientSecret</param>
        /// <param name="expirationInMinutes">Optional. Specify how many minutes until the token expires.</param>
        /// <exception cref="GetTokenException">Thrown if the request for the token fails.</exception>
        /// <returns>Returns a <see cref="Token"/>.</returns>
        public Token GetToken(string clientId, string clientSecret, int? expirationInMinutes=null)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Url);
            var strBuilder = new StringBuilder(string.Format("client_id={0}&grant_type=client_credentials&client_secret={1}", clientId, clientSecret));
            if (expirationInMinutes.HasValue)
            {
                strBuilder.AppendFormat("&expiration={0}", expirationInMinutes.Value);
            }
            uriBuilder.Query = strBuilder.ToString();
            
            var request = HttpWebRequest.Create(uriBuilder.Uri);
            TokenResponse tokenResponse;
            string json;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }
            if (json.Contains("\"error\""))
            {
                var dict = JsonSerializer.DeserializeFromString<Dictionary<string, TokenErrorInfo>>(json);
                throw new GetTokenException(dict["error"]);
            }
            else
            {
                tokenResponse = JsonSerializer.DeserializeFromString<TokenResponse>(json);
            }

            return new Token(tokenResponse);
            
        }
    }
}
