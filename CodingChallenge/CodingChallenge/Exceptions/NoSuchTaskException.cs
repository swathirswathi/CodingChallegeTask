using System.Runtime.Serialization;

namespace CodingChallenge.Exceptions
{
    
    [Serializable]
    internal class NoSuchTaskException : Exception
    {
        public NoSuchTaskException()
        {
        }

        public NoSuchTaskException(string? message) : base(message)
        {
        }

        public NoSuchTaskException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
