using System;
using System.Runtime.Serialization;

namespace TheRavenDB.TextAdventure.Core.Exceptions
{
    [Serializable]
    public class CantParseActionException : Exception
    {
        private const string DefaultErrorMessage = "Could not parse action: {0}";

        public CantParseActionException() { }
        public CantParseActionException(string action) : base(string.Format(DefaultErrorMessage, action)) { }
        public CantParseActionException(string message, Exception inner) : base(message, inner) { }
        protected CantParseActionException( SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}