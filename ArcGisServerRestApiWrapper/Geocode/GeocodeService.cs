using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Esri.ArcGisServer.Rest.Authentication;
using Newtonsoft.Json;

namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// Provides access to an ArcGIS Server geocode service
    /// </summary>
    public class GeocodeService
    {
        /// <summary>
        /// The URI to the map service's REST API endpoint.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="url">The URL of the service. You can omit this parameter if you are using the default Esri World Geocoding Service.</param>
        public GeocodeService(string url = "https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/")
        {
            this.Uri = new Uri(url);
        }

        /// <summary>
        /// Finds any of the following types of locations: Street Adresses, Street Intersections, Points of interest (POI), 
        /// Administrative place names, Postal Codes, or X/Y coordinates.
        /// Geocodes one location per request; the input address can be combined into a single input field or divided among multiple parameters.
        /// </summary>
        /// <param name="parameters">Results are based on the settings passed as GeocodeParameters.</param>
        /// <param name="token">Optional: A valid token is a key that is temporarily related to an ArcGIS Online account.</param>
        /// <returns></returns>
        public GeocodeResult FindAddressCandidates(GeocodeParameters parameters, Token token = null)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Uri);
            uriBuilder.Path += "findAddressCandidates";
            Dictionary<string, object> dict = parameters.ToDictionary();
            string qs = dict.ToQueryString();
            if (token != null)
            {
                qs = string.Format("token={0}&{1}", token.AccessToken, qs);
            }
            uriBuilder.Query = qs;

            var request = HttpWebRequest.Create(uriBuilder.Uri) as HttpWebRequest;
            string json = null;
            using(var response = request.GetResponse() as HttpWebResponse)
	        {
                var stream = response.GetResponseStream();
                using (var streamReader = new StreamReader(stream))
                {
                    json = streamReader.ReadToEnd();
                }
	        }

            //If the request returned an error, throw an exception.
            var errorResponse = JsonConvert.DeserializeObject<GeocodeErrorResponse>(json);
            if (errorResponse.error != null)
            {
                throw new GeocodeException(errorResponse);
            }

            GeocodeResult geocodeResult = null;

            geocodeResult = JsonConvert.DeserializeObject<GeocodeResult>(json);

            return geocodeResult;
        }
    }
}
