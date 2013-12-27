using System;
using System.Runtime.Serialization;

namespace Esri.ArcGisServer.Rest.Authentication
{
    [Serializable]
    public class GetTokenException : Exception
    {
        public TokenErrorInfo ErrorInfo { get; set; }

        public GetTokenException(TokenErrorInfo errorInfo)
            : this(errorInfo.message)
        {
            this.ErrorInfo = errorInfo;
        }
        public GetTokenException(string message) : base(message) { }
        public GetTokenException(string message, Exception inner) : base(message, inner) { }
        protected GetTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
