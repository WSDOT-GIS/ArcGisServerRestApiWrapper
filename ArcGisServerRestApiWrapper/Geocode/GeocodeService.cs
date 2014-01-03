using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="url">The URL of the service. You can omit this parameter if you are using the default.</param>
        public GeocodeService(string url = "http://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/")
        {
            this.Uri = new Uri(url);
        }

        /// <summary>
        /// Finds any of the following types of locations: Street Adresses, Points of interest (POI), Administrative place names,
        /// Postal Codes, or X/Y coordinates.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Find(FindParameters parameters) {
            throw new NotImplementedException();
        }
    }
}
