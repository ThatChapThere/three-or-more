namespace ThreeOrMore;

internal class Die : IRollable
{
    private Random _random = new Random(); // This is the instace of Random that each Die instance will use
    private List<int> _weightings;
    private int _sides;
    private int _value;

    public int NumberOfSides{ get => _sides; }
    public int Value{ get => _value; }
    
    // Default constructor
    public Die(int sides, List<int> weightings)
    {
        if(weightings.Count != sides) // Handle being given the wrong number of weightings
            throw new WrongNumberOfWeightingsException("Not given enough weightings for the amount of faces");

        _sides = sides;
        _weightings = weightings.ToList(); // Calling ToList on a list creates a shallow copy, which is good enough for ints
    }

    // Constructor for a fair die
    public Die(int sides) 
        : this(
            sides,
            Enumerable.Repeat(1, sides).ToList()// create a list of length [sides] filled with ones
        ){}

    // Default die has 6 sides
    public Die() : this(6) {} 

    // Roll the dice - set the value and also return it
    public void Roll()
    {
        double randomValue = _random.NextDouble(); // A random number between 0 and (almost) 1
        double valueToReach = randomValue * _weightings.Sum();

        double cumulativeValue = 0;
        for(int i = 0; i < _weightings.Count; i++)
        {
            cumulativeValue += _weightings[i];

            if(cumulativeValue >= valueToReach){
                _value = i + 1; // add one since lists are 0 indexed and dice are not
                return;
            }
        }

        _value = _sides;
        // just in case a random number very near to 1 is generated
        // and a floating point error makes the above loop not set _value
    }
}