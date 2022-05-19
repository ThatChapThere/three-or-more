namespace ThreeOrMore;

class UserInterface
{
    public static bool MainMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Would you like to play a game of Three or More? ");
        return getYesOrNo();
    }

    public static int getRerollOption(List<int> rerollNumbers)
    {
        while(true)
        {
            try{
                Console.Write("Please choose one of the following numbers to keep ( ");
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
                Console.WriteLine("An error occurred - please try again");
            }
        }
    }

    public static void printDice(List<int> diceValues)
    {
        ConsoleColor oldColour = Console.ForegroundColor;

        Console.Write("The following values on the dice were rolled: ");
        foreach(int diceValue in diceValues)
        {
            setConsoleColorFromNumber(diceValue);
            Console.Write($"{diceValue} ");
        }
        Console.WriteLine();

        Console.ForegroundColor = oldColour;
    }

    public static bool getYesOrNo()
    {
        while(true)
        {
    try{
        var userInput = Console.ReadLine()?? "";
        
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
    }catch(IndexOutOfRangeException){ // If the user just presses enter
        Console.WriteLine("Please enter an answer");
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

    // This also acts as the new screen function for each turn, so
    public static void DisplayScores(List<Player> players)
    {
        Console.Write("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
        Console.Write("The scores are as follows: ");
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

    public static void setConsoleColorFromNumber(int n)
    {
        Console.ForegroundColor = (ConsoleColor) ((n % 15) + 1); // restricts it to the range (1-15) - all except black
    }

    public static void PrintGameFailedToEndProperlyError()
    {
        Console.WriteLine("Error - The game continued after someone won!");
        Console.WriteLine("Hopefully the right player got credit for the win!");
    }

    public static void Goodbye()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\nThanks for playing!");
        Console.ReadKey();
    }
}