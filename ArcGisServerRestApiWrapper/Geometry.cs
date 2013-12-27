using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest
{
    public class Geometry
    {
        public SpatialReference spatialReference { get; set; }

        // Point properties
        public double? x { get; set; }
        public double? y { get; set; }
        public double? z { get; set; }
        public double? m { get; set; }

        // HasM and HasZ are for all types except Point and Envelope.
        public bool? hasM { get; set; }
        public bool? hazZ { get; set; }

        // Multipoint
        public double[][] points { get; set; }


        // Polyline
        public double[][][] paths { get; set; }

        // Polygon
        public double[][][] rings { get; set; }

        // Envelope
        public double? xmin { get; set; }
        public double? ymin { get; set; }
        public double? xmax { get; set; }
        public double? ymax { get; set; }

        public double? zmin { get; set; }
        public double? zmax { get; set; }

        public double? mmin { get; set; }
        public double? mmax { get; set; }


        public GeometryType GetType()
        {
            if (points != null)
            {
                return GeometryType.Multipoint;
            }
            else if (paths != null)
            {
                return GeometryType.Polyline;
            }
            else if (rings != null)
            {
                return GeometryType.Polygon;
            }
            else if (xmin.HasValue && ymin.HasValue && xmax.HasValue && ymax.HasValue)
            {
                return GeometryType.Envelope;
            }
            else
            {
                return GeometryType.Point;
            }
        }
    }

    public enum GeometryType
    {
        Point,
        Multipoint,
        Polyline,
        Polygon,
        Envelope
    }
}
