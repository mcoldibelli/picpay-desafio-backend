namespace api.Exceptions;

public class UserNotfoundException : Exception
{
    public UserNotfoundException()
        : base("User in transaction was not found.")
    {
    }
}
