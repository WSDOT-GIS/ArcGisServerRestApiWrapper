using System;
using System.Runtime.Serialization;

namespace Esri.ArcGisServer.Rest.Geocode
{
    /// <summary>
    /// An exception that is thrown when a call to the findAddressCandidates REST endpoint fails.
    /// </summary>
    [Serializable]
    public class GeocodeException : Exception
    {
        /// <summary>Creates a new instance.</summary>
        public GeocodeException() { }
        /// <summary>Creates a new instance.</summary>
        /// <param name="errorResponse">The REST response.</param>
        public GeocodeException(GeocodeErrorResponse errorResponse) : base(errorResponse.error.message) { }
        /// <summary>Creates a new instance.</summary>
        public GeocodeException(string message) : base(message) { }
        /// <summary>Creates a new instance.</summary>
        public GeocodeException(string message, Exception inner) : base(message, inner) { }
        /// <summary>Creates a new instance.</summary>
        protected GeocodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
