using Common.Enums;

namespace Common.Exceptions;

public class RepositoryException : AppException
{
    public RepositoryException(string message, Exception? innerException = null) : base(ApiResultStatusCode.LogicError, message, innerException)
    {

    }
}