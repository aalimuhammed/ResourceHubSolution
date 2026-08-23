namespace ResourceHub.Application.Exceptions
{
    public class UnAuthenticatedException : Exception
    {
        public UnAuthenticatedException(string message) : base(message)
        {
        }
    }
}