namespace ThreeOrMore;

internal class Game
{
    // Rules
    private List<int> _scoresForEachMatchingSet = new List<int>();
    private List<int> _bonusRollsForEachMatchingSet = new List<int>();
    private int _pointsToWin;

    // Game state
    private List<rerollOption> _rerollOptions = new List<rerollOption>();
    private int _bonusRollsRemaining = 0;
    private Dice _dice;
    private List<Player> _players = new List<Player>();

    public bool Done{ get; }

    public Game(int playerCount, Dice dice, List<int> scoresForEachMatchingSet, int pointsToWin)
    {
        for(int i = 1; i <= playerCount; i++)
            _players.Add(new Player(i));
        
        _dice = dice;
        _scoresForEachMatchingSet = scoresForEachMatchingSet;
        _pointsToWin = pointsToWin;

        // populate the bonus roll rules with 0s up to the possibility of all dice having the same value
        _bonusRollsForEachMatchingSet = Enumerable.Repeat(0, _dice.Values.Count).ToList();
    }

    public void AddBonusRollRule(int value, int rerolls) =>
        _bonusRollsForEachMatchingSet[value] = rerolls;

    public void PlayTurn()
    {
        // if(Done)
        // throw new AttemptToContinueFinishedGameException();
        foreach(Player player in _players)
        {
            _dice.Roll();

            // Key = number rolled, Value = frequency
            var frequencies = _dice.Values
                .GroupBy(v => v) // Group by value
                .ToDictionary(g => g.Key, g => g.Count());
                // Group keys are what the group object was grouped by, in this case value
                // The count is the size of these groups

            int maxFrequency = frequencies.Max(d => d.Value);

            _bonusRollsRemaining = _bonusRollsForEachMatchingSet[maxFrequency];
            player.Score += _scoresForEachMatchingSet[maxFrequency];

            while(_bonusRollsRemaining != 0)
            {
                _rerollOptions = frequencies
                    .Where(freq => freq.Value == maxFrequency) // For every number that occurs (twice) enough to make rerolling an option
                    .Select(
                        freq => new rerollOption(
                            _dice.Values
                                .Select(dieValue => !(dieValue == freq.Key)) // all dice that aren't that number can be rerolled
                                .ToList(), 
                            freq.Key
                        )
                    ).ToList();
                
                
                if(_rerollOptions.Count > 1)
                {

                }


                _bonusRollsRemaining--;
            }
        }
    }



    // This is in case the player rolls two pairs
    // (or any other set that could lead to a reroll with other rules)
    private struct rerollOption
    {
        public List<bool> Dice { get; }
        public int Value { get; }

        public rerollOption(List<bool> dice, int value)
        {
            Dice = dice;
            Value = value;
        }
    }
}