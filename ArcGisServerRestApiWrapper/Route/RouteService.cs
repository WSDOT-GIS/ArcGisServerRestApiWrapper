using Esri.ArcGisServer.Rest.Authentication;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    public class RouteService
    {
        /// <summary>
        /// The URI to the map service's REST API endpoint.
        /// </summary>
        public Uri Uri { get; set; }

        public RouteService(string url = "http://route.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World/")
        {

        }

        /// <summary>
        /// Determines a route between two or more points.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SolveException"></exception>
        public object Solve(SolveParameters parameters, Token token)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Uri);
            string qs = parameters.ToQueryString();
            if (token != null)
            {
                qs = string.Format("{0}&token={1}", qs, token.AccessToken);
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
            var errorResponse = JsonSerializer.DeserializeFromString<SolveErrorResponse>(json);
            if (errorResponse.error != null)
            {
                throw new SolveException(errorResponse);
            }

            SolveResult solveResult = null;

            solveResult = JsonSerializer.DeserializeFromString<SolveResult>(json);


            return solveResult;
        }
    }
}
