using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    public class RouteService
    {
        /// <summary>
        /// The URI to the map service's REST API endpoint.
        /// </summary>
        public Uri Uri { get; set; }

        public object Solve(SolveParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
