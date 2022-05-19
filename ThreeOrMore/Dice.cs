namespace ThreeOrMore;

internal class Dice : IRollable
{
    private List<Die> _dice = new List<Die>();
    
    public int DiceCount{ get => _dice.Count; }
    public List<int> Values{
        get => 
            _dice
                .Select(d => d.Value)
                .ToList();
    }

    // Weighted dice
    public Dice(int count, List<int> sides, List<List<int>> weightings)
    {
        if(weightings.Count != count)
            throw new WrongNumberOfWeightingsException("Not given enough weighting sets for the amount of dice");
        
        for(int i = 0; i < count; i++)
            _dice.Add(new WeightedDie(sides[i], weightings[i]));
    }

    // Fair dice
    public Dice(int count, List<int> sides) 
    {
        for(int i = 0; i < count; i++)
            _dice.Add(new FairDie(sides[i]));
    }

    // Fair dice all with the same number of sides
    public Dice(int count, int sides): this(
        count,
        Enumerable.Repeat(sides, count).ToList()
    ){}
    
    public void Roll(List<bool> shouldRoll)
    {
        if(shouldRoll.Count != _dice.Count)
            throw new WrongNumberOfRollSpecifiersException();

        for(int i = 0; i < shouldRoll.Count; i++)
        {
            if(shouldRoll[i])
                _dice[i].Roll();
        }
    }

    public void Roll() =>
        _dice.ForEach(d => d.Roll());
}