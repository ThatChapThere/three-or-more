namespace ThreeOrMore;

public class InvalidNumberException : Exception
{
    public InvalidNumberException()
    {
    }

    public InvalidNumberException(string message)
        : base(message)
    {
    }

    public InvalidNumberException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
