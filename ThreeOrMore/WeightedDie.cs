namespace ThreeOrMore;

internal class WeightedDie : Die, IRollable
{
    private List<int> _weightings;

    public WeightedDie(int sides, List<int> weightings) : base(sides)
    {
        if(weightings.Count != sides) // Handle being given the wrong number of weightings
            throw new WrongNumberOfWeightingsException("Not given enough weightings for the amount of faces");

        _weightings = weightings.ToList(); // Calling ToList on a list creates a shallow copy, which is good enough for ints
    }

    // Roll the dice - set the value and also return it
    public override void Roll()
    {
        double randomValue = _random.NextDouble(); // A random number between 0 and (almost) 1
        double valueToReach = randomValue * _weightings.Sum();

        double cumulativeValue = 0;
        for(int i = 0; i < _weightings.Count; i++)
        {
            cumulativeValue += _weightings[i];

            if(cumulativeValue >= valueToReach){
                _value = i + 1; // add one since lists are 0 indexed and dice are not (except d10s in D&D)
                return;
            }
        }

        _value = _sides;
        // just in case a random number very near to 1 is generated
        // and a floating point error makes the above loop not set _value
        // I have no idea if this is actually possible
    }
}