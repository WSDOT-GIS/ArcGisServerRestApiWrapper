using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// The results of a <see cref="RouteService.Solve"/> operation.
    /// </summary>
    public class SolveResult
    {
        public Direction[] directions { get; set; }
        public FeatureSet routes { get; set; }
        public FeatureSet stops { get; set; }
        public FeatureSet barriers { get; set; }
        public Message[] messages { get; set; }
    }

    public class Message
    {
        public string type { get; set; }
        public string description { get; set; }
    }

    public class Direction
    {
        public int routeId { get; set; }
        public string routeName { get; set; }
        public DirectionSummary summary { get; set; }
        public bool hasZ { get; set; }
        public bool hasM { get; set; }
        public Feature[] features { get; set; }
    }

    public class DirectionSummary
    {
        public double totalLength { get; set; }
        public double totalTime { get; set; }
        public double totalDriveTime { get; set; }
        public Geometry envelope { get; set; }
    }


}
