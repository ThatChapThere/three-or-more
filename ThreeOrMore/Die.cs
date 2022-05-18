namespace ThreeOrMore;

internal abstract class Die : IRollable
{
    protected Random _random = new Random(); // This is the instace of Random that each Die instance will use
    protected int _sides;
    protected int _value;

    public int NumberOfSides{ get => _sides; }
    public int Value{ get => _value; }
    
    // Default constructor (fair die)
    public Die(int sides) 
    {
        _sides = sides;
    }

    // 6 sided die
    public Die() : this(6) {} 

    // Roll the dice - set the value and also return it
    public abstract void Roll();
}