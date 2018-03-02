using System.Collections.Generic;

namespace Esri.ArcGisServer.Rest.Geocode
{
    public class GeocodeResult
    {
        /// <summary>
        /// The spatial reference of the output match location coordinates as specified by the wkid and latestWkid properties.
        /// The outSR input parameter determines the spatial reference. This is always returned by default.
        /// </summary>
        public SpatialReference spatialReference { get; set; }

        /// <summary>
        /// The possible matching addresses. When the service cannot find any matches for a request, a null set is returned. 
        /// </summary>
        public Candidate[] candidates { get; set; }
    }

    /// <summary>
    /// An individual address candidate
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Complete matching address returned for findAddressCandidates and geocodeAddresses geocode requests. 
        /// This is always returned by default.
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// <para>The point coordinates of the output match location as specified by the x and y properties.</para>
        /// The spatial reference of the x and y coordinates is defined by the spatialReference output field. 
        /// Always returned by default for findAddressCandidates and geocodeAddresses geocode requests only. 
        /// Refer to the description of the locationType parameter for more information about how the location output field relates to the X and Y output attributes.
        /// </summary>
        public Geometry location { get; set; }

        /// <summary>
        /// <para>Only returned for geocodeAddresses requests.</para>
        /// Each record in a batch geocode response includes a ResultID value, which equals the OBJECTID value of the corresponding input address record. 
        /// It can be used to join the output fields in the response to the attributes in the original address table.
        /// </summary>
        public string ResultID { get; set; }

        /// <summary>
        /// <para>A number from 1–100 indicating the degree to which the input tokens in a geocoding request match the address components in a candidate record. </para>
        /// A score of 100 represents a perfect match, while lower scores represent decreasing match accuracy. Score is always returned by default.
        /// </summary>
        public double score { get; set; }

        /// <summary>
        /// Attributes associated with this feature.
        /// </summary>
        public Dictionary<string, object> attributes { get; set; }

        /// <summary>
        /// The minimum bounding rectangle of the output match feature as specified by the xmin, ymin, xmax, and ymax properties. 
        /// The spatial reference of the x and y coordinates is defined by the spatialReference output field. 
        /// This is always returned by default for findAddressCandidates geocode requests only.
        /// </summary>
        public Geometry extent { get; set; }
    }
}
