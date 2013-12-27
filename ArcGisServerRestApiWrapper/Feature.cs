using System.Collections.Generic;

namespace Esri.ArcGisServer.Rest
{
    public class Feature
    {
        public Dictionary<string, object> attributes { get; set; }
        public Geometry geometry { get; set; }
        public string compressedGeometry { get; set; }
    }
}
