using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Route
{
    [Serializable]
    public class SolveException : Exception
    {
        public SolveException() { }
        public SolveException(SolveErrorResponse errorResponse) : base(errorResponse.error.message)
        {

        }
        public SolveException(string message) : base(message) { }
        public SolveException(string message, Exception inner) : base(message, inner) { }
        protected SolveException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
