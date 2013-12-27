using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest
{
    public class Feature
    {
        public Dictionary<string, object> attributes { get; set; }
        public Geometry geometry { get; set; }
        public string compressedGeometry { get; set; }
    }
}
