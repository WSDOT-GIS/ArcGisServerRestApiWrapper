using System;
using System.Runtime.Serialization;

namespace Esri.ArcGisServer.Rest.Authentication
{
    /// <summary>
    /// An exception that occurs if there is an error getting a token.
    /// </summary>
    [Serializable]
    public class GetTokenException : Exception
    {
        /// <summary>
        /// Information about the error returned from the token service.
        /// </summary>
        public TokenErrorInfo ErrorInfo { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorInfo"></param>
        public GetTokenException(TokenErrorInfo errorInfo)
            : this(errorInfo.message)
        {
            this.ErrorInfo = errorInfo;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public GetTokenException(string message) : base(message) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public GetTokenException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected GetTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
