namespace ThreeOrMore;

class Test
{
    public static bool TestAll()
    {
        return
            RunTestAndPrintResult(canRollNDice()) &&
            RunTestAndPrintResult(DiceAreFair()) &&
            RunTestAndPrintResult(DiceAreUnique()) &&
            RunTestAndPrintResult(canReachFixedWinScore());
    }

    public static bool RunTestAndPrintResult(bool TestResult)
    {
        if(TestResult)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\ntest succeeded\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\ntest failed\n");
        }
        Console.ForegroundColor = ConsoleColor.White;
        return TestResult;
    }

    public static bool canRollNDice()
    {
        Console.WriteLine("Testing that the number of dice can be customised successfully");
        Console.WriteLine("Expected output: 1 2 3 4 5");
        Console.Write    ("Actual   output:");
        for(int i = 1; i <= 5; i++)
        {
            Dice dice = new Dice(i, 6);
            Console.Write($" {dice.Values.Count}");
            if(dice.Values.Count != i)
                return false;
        }
        return true;
    }

    public static bool DiceAreFair()
    {
        Console.WriteLine("Testing that the dice are fair");

        var die = new FairDie(6);

        var frequencies = new int[6];

        int numberOfDice = (int) 1e6;
        int reasonableDifference = 2000;
        
        for(int i = 0; i < numberOfDice; i++)
        {
            die.Roll();
            frequencies[die.Value - 1]++;
        }

        for(int i = 0; i < frequencies.Length; i++)
        {
            Console.Write("The number ");
            UserInterface.setConsoleColorFromNumber(i);
            Console.Write(i + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" occurs {frequencies[i]} times");
        }

        var differences = frequencies
            .ToList()
            .Select(f => Math.Abs(f - numberOfDice / 6))
            .ToList();

        for(int i = 0; i < differences.Count; i++)
        {
            Console.Write("The number ");
            UserInterface.setConsoleColorFromNumber(i);
            Console.Write(i + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" is ");
            Console.ForegroundColor = 
                differences[i] <= reasonableDifference ? 
                    ConsoleColor.Green:
                    ConsoleColor.Red;

            Console.Write(differences[i]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" away from the expected frequency");
        }

        return differences
            .Where(x => x > reasonableDifference)
            .Count() == 0;
    }

    public static bool DiceAreUnique()
    {
        Console.WriteLine("Testing that rolling a set of dice yields unique values: ");
        var dice = new Dice(5, 6);

        for(int i = 0; i < 10; i++)
        {
            dice.Roll();
            UserInterface.printDice(dice.Values);
            if(
                dice.Values
                    .GroupBy(v => v)
                    .Count() > 1
            ){
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Unique Dice");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }else{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Identical Dice");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        return false;
    }

    public static bool canReachFixedWinScore()
    {
        Console.WriteLine("Testing that the game can be won with a sufficient score: ");

        // One player, one die, one point to win, one point per turn
        var game = new Game(
            1,
            new Dice(1, 2),
            new List<int> { 1 },
            1
        );
        game.PlayTurn();
        return game.Done;
    }
}