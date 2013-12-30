namespace Esri.ArcGisServer.Rest
{
    /// <summary>
    /// Represents a geometry.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// The spatial reference of the geometry.
        /// </summary>
        public SpatialReference spatialReference { get; set; }

        // HasM and HasZ are for all types except Point and Envelope.
        /// <summary>Indicates if the geometry has M information. Does not apply to Point or Envelope geometry.</summary>
        public bool? hasM { get; set; }
        /// <summary>Indicates if the geometry has Z information. Does not apply to Point or Envelope geometry.</summary>
        public bool? hazZ { get; set; }

        #region Point properties
        /// <summary>The X value of a Point geometry.</summary>
        public double? x { get; set; }
        /// <summary>The Y value of a Point geometry.</summary>
        public double? y { get; set; }
        /// <summary>The Z value of a Point geometry.</summary>
        public double? z { get; set; }
        /// <summary>The M value of a Point geometry.</summary>
        public double? m { get; set; }
        #endregion

        /// <summary>The points contained in a Multipoint geometry.</summary>
        public double[][] points { get; set; }

        /// <summary>The paths of a Polyline geometry.</summary>
        public double[][][] paths { get; set; }

        /// <summary>The rings of a polygon geometry.</summary>
        public double[][][] rings { get; set; }

        #region Envelope properties
        /// <summary>The xmin property of an envelope.</summary>
        public double? xmin { get; set; }
        /// <summary>The ymin property of an envelope.</summary>
        public double? ymin { get; set; }
        /// <summary>The xmax property of an envelope.</summary>
        public double? xmax { get; set; }
        /// <summary>The ymax property of an envelope.</summary>
        public double? ymax { get; set; }

        /// <summary>The zmin property of an envelope.</summary>
        public double? zmin { get; set; }
        /// <summary>The zmax property of an envelope.</summary>
        public double? zmax { get; set; }

        /// <summary>The mmin property of an envelope.</summary>
        public double? mmin { get; set; }
        /// <summary>The mmax property of an envelope.</summary>
        public double? mmax { get; set; } 
        #endregion


        /// <summary>
        /// Returns the type of geometry.
        /// </summary>
        /// <returns>Returns a value indicating the type of geometry.</returns>
        /// <remarks>
        /// The geometry type is determined by the presence of different properties.
        /// </remarks>
        public GeometryType GetGeometryType()
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

    /// <summary>
    /// Indicates the type of geometry.
    /// </summary>
    public enum GeometryType
    {
        ///<summary>Point</summary>
        Point,
        ///<summary>Multipoint</summary>
        Multipoint,
        ///<summary>Polyline</summary>
        Polyline,
        ///<summary>Polygon</summary>
        Polygon,
        ///<summary>Envelope</summary>
        Envelope
    }
}
