namespace ThreeOrMore;

internal class Program
{
    public static int players = 2;
    public static int numberOfDice = 5;
    public static int typeOfDice = 6;
    public static List<int> dieCountScores = new List<int>() { 0, 0, 3, 6, 12};
    public static int winningScore = 50;

    static void Main(string[] args)
    {
        while(UserInterface.MainMenu())
        {
            PlayGame();
        }

        UserInterface.Goodbye();
    }

    static void PlayGame()
    {
        var game = new Game(
            players,
            new Dice(numberOfDice, 6),
            dieCountScores,
            winningScore
        );
        game.AddBonusRollRule(2, 2);

        while(!game.Done)
            game.PlayTurn();
    }
}