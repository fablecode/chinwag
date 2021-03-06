using System;
using System.Runtime.Serialization;

namespace Identity.DatabaseMigration.Exceptions
{
    public sealed class DatabaseMigrationException : Exception
    {
        public DatabaseMigrationException()
        {
        }

        public DatabaseMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DatabaseMigrationException(string? message) : base(message)
        {
        }

        public DatabaseMigrationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}