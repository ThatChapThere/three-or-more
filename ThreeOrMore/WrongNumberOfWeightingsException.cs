namespace ThreeOrMore;

public class WrongNumberOfWeightingsException : Exception
{
    public WrongNumberOfWeightingsException()
    {
    }

    public WrongNumberOfWeightingsException(string message)
        : base(message)
    {
    }

    public WrongNumberOfWeightingsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
