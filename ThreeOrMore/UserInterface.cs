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
        Console.Write("The following values on the dice were rolled: ");
        diceValues.ForEach(v => Console.Write($"{v} "));
        Console.WriteLine();
    }

    public static bool getYesOrNo()
    {
        while(true)
        {
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
        }
    }

    public static bool askShouldReroll()
    {
        Console.Write("Would you like to re-roll? ");
        return getYesOrNo();
    }
}