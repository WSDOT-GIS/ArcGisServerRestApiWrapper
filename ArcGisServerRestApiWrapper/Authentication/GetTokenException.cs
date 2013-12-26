using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Esri.ArcGisServer.Rest.Authentication
{
    [Serializable]
    public class GetTokenException : Exception
    {
        public TokenErrorInfo ErrorInfo { get; set; }

        public GetTokenException(TokenErrorInfo errorInfo): this(errorInfo.message) {
            this.ErrorInfo = errorInfo;
        }
        public GetTokenException(string message) : base(message) { }
        public GetTokenException(string message, Exception inner) : base(message, inner) { }
        protected GetTokenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
