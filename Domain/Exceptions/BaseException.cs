namespace Domain.Exceptions
{
    public abstract class BaseException(string message, string code) : Exception(message)
    {
        public string Code { get; } = code;
    }
}
