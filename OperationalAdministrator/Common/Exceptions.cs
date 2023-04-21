namespace OperationalAdministrator.Common
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
    public class ServerErrorException : Exception
    {
        public ServerErrorException(string message) : base(message) { }
    }
    public class DuplicatedEntryException : Exception
    {
        public DuplicatedEntryException(string message) : base(message) { }
    }
}
