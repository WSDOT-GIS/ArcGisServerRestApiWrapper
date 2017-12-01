using Esri.ArcGisServer.Rest.Authentication;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// Provides access to an ArcGIS Server route (NAServer) service.
    /// </summary>
    public class RouteService
    {
        /// <summary>
        /// The URI to the map service's REST API endpoint.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="url">The URL of the service. You can omit this parameter if you are using the default.</param>
        public RouteService(string url = "http://route.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World/")
        {
            this.Uri = new Uri(url);
        }

        /// <summary>
        /// Determines a route between two or more points.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SolveException"></exception>
        public SolveResult Solve(SolveParameters parameters, Token token)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Uri);
            uriBuilder.Path += "solve";
            string qs = parameters.ToQueryString();
            if (token != null)
            {
                qs = string.Format("token={1}&{0}&f=json", qs, token.AccessToken);
            }
            uriBuilder.Query = qs;

            var request = HttpWebRequest.Create(uriBuilder.Uri) as HttpWebRequest;
            string json = null;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var stream = response.GetResponseStream();
                using (var streamReader = new StreamReader(stream))
                {
                    json = streamReader.ReadToEnd();
                }
            }

            // If the request returned an error, throw an exception.
            var errorResponse = JsonConvert.DeserializeObject<SolveErrorResponse>(json);
            if (errorResponse.error != null)
            {
                throw new SolveException(errorResponse);
            }

            SolveResult solveResult = null;

			solveResult = JsonConvert.DeserializeObject<SolveResult>(json);


            return solveResult;
        }
    }
}
