namespace Libraries.Web.Common.Exceptions;

public class UserException : Exception
{
    public UserException(string message) : base(message)
    {
    }
}