namespace Esri.ArcGisServer.Rest
{
    /// <summary>
    /// A set of <see cref="Feature"/> objects.
    /// </summary>
    public class FeatureSet
    {
        /// <summary>
        /// A common spatial reference that applies to all <see cref="FeatureSet.features"/>.
        /// </summary>
        public SpatialReference spatialReference { get; set; }
        /// <summary>
        /// Indicates if there is elevation information.
        /// </summary>
        public bool? hasZ { get; set; }
        /// <summary>
        /// Indicates if there is measure information. (E.g., 5 miles along a route.)
        /// </summary>
        public bool? hasM { get; set; }
        /// <summary>
        /// The features associated with this feature set.
        /// </summary>
        public Feature[] features { get; set; }
    }
}
