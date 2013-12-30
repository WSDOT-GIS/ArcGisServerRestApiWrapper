using System.Collections.Generic;

namespace Esri.ArcGisServer.Rest
{
    /// <summary>
    /// A geometry and associated attributes.
    /// </summary>
    /// <remarks>
    /// A <see cref="Feature"/> will either contain The feature will either contain <see cref="Feature.geometry"/> 
    /// or <see cref="Feature.compressedGeometry"/>, but not both.
    /// </remarks>
    public class Feature
    {
        /// <summary>
        /// Attributes associated with this feature.
        /// </summary>
        public Dictionary<string, object> attributes { get; set; }
        /// <summary>
        /// Geometry. The feature will either contain this property or <see cref="Feature.compressedGeometry"/>, but not both.
        /// </summary>
        public Geometry geometry { get; set; }
        /// <summary>
        /// A compressed alternative to <see cref="Feature.geometry"/>.
        /// </summary>
        public string compressedGeometry { get; set; }
    }
}
