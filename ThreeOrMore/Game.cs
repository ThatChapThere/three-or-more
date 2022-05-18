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
    private bool _done = false;

    public bool Done{ get => _done; }

    public Game(int playerCount, Dice dice, List<int> scoresForEachMatchingSet, int pointsToWin)
    {
        for(int i = 1; i <= playerCount; i++)
            _players.Add(new Player(UserInterface.getPlayerName(i)));
        
        _dice = dice;
        _scoresForEachMatchingSet = scoresForEachMatchingSet;
        _pointsToWin = pointsToWin;

        // populate the bonus roll rules with 0s up to the possibility of all dice having the same value
        _bonusRollsForEachMatchingSet = Enumerable.Repeat(0, _dice.Values.Count).ToList();
    }

    public void AddBonusRollRule(int matchingSet, int rerolls) =>
        _bonusRollsForEachMatchingSet[matchingSet - 1] = rerolls; 
        // lists are zero indexed but frequecies start at 1,
        // so we subtract 1

    public void PlayTurn()
    {
        // if(Done)
        // throw new AttemptToContinueFinishedGameException();
        foreach(Player player in _players)
        {
            Console.ForegroundColor = (ConsoleColor) ((_players.IndexOf(player) % 15) + 1); // everything but black
            UserInterface.DisplayScores(_players);
            UserInterface.NotifyTurn(player);
            UserInterface.RollDice();

            _dice.Roll();
            UserInterface.printDice(_dice.Values);

            // Key = number rolled, Value = frequency
            var frequencies = getFrequencies();

            int maxFrequency = frequencies.Max(d => d.Value);
            
            // lists are zero indexed but frequecies start at 1,
            // so we subtract 1
            _bonusRollsRemaining = _bonusRollsForEachMatchingSet[maxFrequency - 1];
            player.Score += _scoresForEachMatchingSet[maxFrequency - 1];

            // Win condition
            if(player.Score > _pointsToWin)
            {
                _done = true;
                UserInterface.CongratulateOnWin(player);
                UserInterface.DisplayScores(_players);
                return;
            }

            while(_bonusRollsRemaining > 0)
            {
                // Let the player choose not to reroll
                if(!UserInterface.askShouldReroll()) break;

                // Set up the reroll options
                setRerollOptionsFromFrequencies(frequencies, maxFrequency);
                
                // Set up a list of which dice to reroll
                List<bool> diceToReroll;

                // If there are multiple ways to reroll
                if(_rerollOptions.Count > 1)
                {
                    int rerollChoice = UserInterface.getRerollOption( // ask the user which one to do
                        _rerollOptions
                            .Select(o => o.Value) // use the list of dice values that appear (twice)
                            .ToList()
                    );

                    // Get the dice to reroll based on user choice of die face value
                    diceToReroll = _rerollOptions
                        .Where(o => o.Value == rerollChoice)
                        .Select(o => o.Dice)
                        .ToList()[0];
                        // There's only one option per face value so after we select
                        // we can just get the only item with [0]

                }else diceToReroll = _rerollOptions[0].Dice;
                
                // Roll dice along with appropriate UI calls
                UserInterface.RollDice();
                _dice.Roll(diceToReroll);
                UserInterface.printDice(_dice.Values);

                // After the reroll, we check whether we scored
                // Add the player score and break if we did
                frequencies = getFrequencies();
                maxFrequency = frequencies.Max(d => d.Value);

                // lists are zero indexed but frequecies start at 1,
                // so we subtract 1
                int score = _scoresForEachMatchingSet[maxFrequency - 1];
                player.Score += score;
                if(score != 0) break;

                _bonusRollsRemaining--;
            }
        }
    }

    // This is in case the player rolls two (or more) pairs
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

    private Dictionary<int, int> getFrequencies()
    {
        // Key = number rolled, Value = frequency
        return _dice.Values
            .GroupBy(v => v) // Group by value
            .ToDictionary(g => g.Key, g => g.Count());
            // Group keys are what the group object was grouped by, in this case value
            // The count is the size of these groups
    }

    private void setRerollOptionsFromFrequencies(Dictionary<int, int> frequencies, int maxFrequency)
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
    }
}