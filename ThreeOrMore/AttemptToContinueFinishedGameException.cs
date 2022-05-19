namespace ThreeOrMore;

public class AttemptToContinueFinishedGameException : Exception
{
    public AttemptToContinueFinishedGameException()
    {
    }

    public AttemptToContinueFinishedGameException(string message)
        : base(message)
    {
    }

    public AttemptToContinueFinishedGameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
