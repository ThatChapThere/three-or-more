namespace ThreeOrMore;

public class WrongNumberOfRollSpecifiersException : Exception
{
    public WrongNumberOfRollSpecifiersException()
    {
    }

    public WrongNumberOfRollSpecifiersException(string message)
        : base(message)
    {
    }

    public WrongNumberOfRollSpecifiersException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
