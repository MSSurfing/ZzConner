using System;
using System.Runtime.Serialization;

namespace Zz.Core.Exceptions
{
    [Serializable]
    public class ZzException : Exception
    {
        public ZzException() { }
        public ZzException(string message) : base(message) { }

        public ZzException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args)) { }

        protected ZzException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ZzException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
