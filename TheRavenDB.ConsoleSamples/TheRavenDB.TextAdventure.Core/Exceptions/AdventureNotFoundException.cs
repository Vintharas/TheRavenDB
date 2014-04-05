using System;
using System.Runtime.Serialization;

namespace TheRavenDB.TextAdventure.Core.Exceptions
{
    [Serializable]
    public class AdventureNotFoundException : Exception
    {
        private const string ErrorMessage = "This is not the adventure you are looking for...";

        public AdventureNotFoundException() : base(ErrorMessage) {}
        public AdventureNotFoundException(string message) : base(message) {}
        public AdventureNotFoundException(string message, Exception inner) : base(message, inner) {}
        protected AdventureNotFoundException( SerializationInfo info, StreamingContext context) : base(info, context) {}

    }
}