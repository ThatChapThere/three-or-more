namespace ThreeOrMore;

class UserInterface
{
    public static int getRerollOption(List<int> rerollNumbers)
    {
        while(true)
        {
            try{
                Console.Write("Please one of the following numbers to keep ( ");
                rerollNumbers.ForEach(n => Console.Write($"{n} "));
                Console.Write("): ");

                int userInput = int.Parse(Console.ReadLine()?? "");

                if(!rerollNumbers.Contains(userInput))
                    throw new InvalidNumberException();

                return userInput;
            }catch(FormatException){
                Console.WriteLine("Please enter a number");
            }catch(InvalidNumberException){
                Console.WriteLine("That number cannot be kept");
            }catch{
                Console.WriteLine("An error occured - please try again");
            }
        }
    }

    public static void printDice(List<int> diceValues)
    {
        ConsoleColor oldColour = Console.ForegroundColor;

        Console.Write("The following values on the dice were rolled: ");
        foreach(int diceValue in diceValues)
        {
            Console.ForegroundColor = (ConsoleColor) ((diceValue % 15) + 1); // everything but black
            Console.Write($"{diceValue} ");
        }
        Console.WriteLine();

        Console.ForegroundColor = oldColour;
    }

    public static bool getYesOrNo()
    {
        while(true)
        {
            var userInput = Console.ReadLine()?? "";
            
            if(userInput.Length == 0) continue; // This prevents a index out of bounds error from occuring below
            switch(userInput.ToUpper()[0])
            {
                case 'N':
                    return false;
                case 'Y':
                    return true;
                default:
                    Console.WriteLine("Not a valid response - please try again");
                    break;
            }
        }
    }

    public static bool askShouldReroll()
    {
        Console.Write("Would you like to re-roll? ");
        return getYesOrNo();
    }

    public static void NotifyTurn(Player player)
    {
        Console.WriteLine($"\n{player.Name} - it's your turn to roll");
    }

    public static void RollDice()
    {
        Console.Write("Press any key to roll");
        Console.ReadKey();
        Console.WriteLine();
    }

    public static void DisplayScores(List<Player> players)
    {
        Console.Write("\n\nThe scores are as follows: ");
        players.ForEach(
            p => Console.Write($"{p.Name}: {p.Score} ")
        );
        Console.WriteLine();
    }

    public static string getPlayerName(int playerNumber)
    {
        Console.Write($"Please enter the name for Player {playerNumber}: ");
        while(true)
        {
            try{
                string name = Console.ReadLine() ?? "";
                if(name == "")
                    throw new PlayerUnnamedException();
                return name;
            }catch(IOException){
                Console.WriteLine("IO Error - please reenter your name");
            }catch(OutOfMemoryException){
                Console.WriteLine("Memory Error - please reenter your name");
            }catch(ArgumentOutOfRangeException){
                Console.WriteLine("Could not take in player name - please reenter your name");
            }catch(PlayerUnnamedException){
                Console.WriteLine("Player name may not be blank");
            }
        }
    }

    public static void CongratulateOnWin(Player player)
    {
        Console.WriteLine($"Congratulations, {player.Name} - You Win!");
    }
}