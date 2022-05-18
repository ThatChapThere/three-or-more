namespace ThreeOrMore;

public class PlayerUnnamedException : Exception
{
    public PlayerUnnamedException()
    {
    }

    public PlayerUnnamedException(string message)
        : base(message)
    {
    }

    public PlayerUnnamedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
