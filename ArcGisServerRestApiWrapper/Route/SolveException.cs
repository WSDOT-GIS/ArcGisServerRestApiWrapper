using System;
using System.Runtime.Serialization;

namespace Esri.ArcGisServer.Rest.Route
{
    /// <summary>
    /// An exception that is thrown when a call to the solve REST endpoint fails.
    /// </summary>
    [Serializable]
    public class SolveException : Exception
    {
        /// <summary>Creates a new instance.</summary>
        public SolveException() { }
        /// <summary>Creates a new instance.</summary>
        /// <param name="errorResponse">The REST response.</param>
        public SolveException(SolveErrorResponse errorResponse) : base(errorResponse.error.message) { }
        /// <summary>Creates a new instance.</summary>
        public SolveException(string message) : base(message) { }
        /// <summary>Creates a new instance.</summary>
        public SolveException(string message, Exception inner) : base(message, inner) { }
        /// <summary>Creates a new instance.</summary>
        protected SolveException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
