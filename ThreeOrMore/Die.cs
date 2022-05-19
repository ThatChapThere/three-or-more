namespace ThreeOrMore;

internal abstract class Die : IRollable
{
    protected Random _random = new Random(); // This is the instance of Random that each Die instance will use
    protected int _sides;
    protected int _value;

    public int NumberOfSides{ get => _sides; }
    public int Value{ get => _value; }
    
    // Default constructor
    public Die(int sides) 
    {
        _sides = sides;
    }

    // Roll the dice - set the value and also return it
    public abstract void Roll();
}