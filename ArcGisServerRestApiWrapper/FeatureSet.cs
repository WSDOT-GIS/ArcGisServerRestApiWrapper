using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest
{
    public class FeatureSet
    {
        public SpatialReference spatialReference { get; set; }
        public bool hasZ { get; set; }
        public bool hasM { get; set; }
        public Feature[] features { get; set; }
    }
}
