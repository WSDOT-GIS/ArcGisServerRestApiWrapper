using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest
{
    public abstract class Geometry
    {
        public SpatialReference spatialReference { get; set; }
    }

    public class Point : Geometry
    {
        public double? x { get; set; }
        public double? y { get; set; }
        public double? z { get; set; }
        public double? m { get; set; }

        public double?[] ToArray()
        {
            var output = new List<double?>();
            output.Add(this.x);
            output.Add(this.y);
            if (this.z.HasValue || this.m.HasValue)
            {
                output.Add(this.z);
                if (this.m.HasValue)
                {
                    output.Add(this.m);
                }
            }
            return output.ToArray();
        }
    }

    public class Multipoint : Geometry
    {
        public bool hasM { get; set; }
        public bool hazZ { get; set; }
        public double[][] points { get; set; }
    }

    public class Polyline : Geometry
    {
        public bool hasM { get; set; }
        public bool hasZ { get; set; }
        public double[][][] paths { get; set; }
    }

    public class Polygon : Geometry
    {
        public bool hasM { get; set; }
        public bool hasZ { get; set; }
        public double[][][] rings { get; set; }
    }

    public class Envelope: Geometry
    {
        public double xmin { get; set; }
        public double ymin { get; set; }
        public double xmax { get; set; }
        public double ymax { get; set; }

        public double? zmin { get; set; }
        public double? zmax { get; set; }

        public double? mmin { get; set; }
        public double? mmax { get; set; }

    }
}
