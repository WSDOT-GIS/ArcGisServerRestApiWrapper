
namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// The results of a <see cref="RouteService.Solve"/> operation.
    /// </summary>
    public class SolveResult
    {
        /// <summary>
        /// Directions. (e.g., turn left at 1st. street).
        /// </summary>
        public Direction[] directions { get; set; }
        /// <summary>
        /// Route features.
        /// </summary>
        public FeatureSet routes { get; set; }
        /// <summary>
        /// Stop features.
        /// </summary>
        public FeatureSet stops { get; set; }
        /// <summary>
        /// Barrier features.
        /// </summary>
        public FeatureSet barriers { get; set; }
        /// <summary>
        /// Messages.
        /// </summary>
        public Message[] messages { get; set; }
    }

    /// <summary>
    /// Messages associated with a <see cref="SolveResult"/>.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The type of message.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The message description.
        /// </summary>
        public string description { get; set; }
    }

    /// <summary>
    /// One step in a list of directions of a <see cref="SolveResult"/>.
    /// </summary>
    public class Direction
    {
        /// <summary>
        /// Route ID.
        /// </summary>
        public int routeId { get; set; }
        /// <summary>
        /// RouteName
        /// </summary>
        public string routeName { get; set; }
        /// <summary>
        /// Summary information about this step in the directions.
        /// </summary>
        public DirectionSummary summary { get; set; }
        /// <summary>
        /// Indicates if there is elevation (z) information.
        /// </summary>
        public bool hasZ { get; set; }
        /// <summary>
        /// Indicates if there is measure information.
        /// </summary>
        public bool hasM { get; set; }
        /// <summary>
        /// An array of features.
        /// </summary>
        public Feature[] features { get; set; }
    }

    /// <summary>
    /// Summary information about a <see cref="Direction"/>.
    /// </summary>
    public class DirectionSummary
    {
        /// <summary>
        /// Total length
        /// </summary>
        public double totalLength { get; set; }
        /// <summary>
        /// Total time.
        /// </summary>
        public double totalTime { get; set; }
        /// <summary>
        /// Total drive time.
        /// </summary>
        public double totalDriveTime { get; set; }
        /// <summary>
        /// Envelope that contains the <see cref="Direction.features"/>.
        /// </summary>
        public Geometry envelope { get; set; }
    }


}
