namespace ThreeOrMore;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new Game(
            2,
            new Dice(5),
            new List<int>() { 0, 0, 3, 6, 12},
            50
        );
        
        game.AddBonusRollRule(2, 2);
    }
}