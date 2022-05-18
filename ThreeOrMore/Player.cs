namespace ThreeOrMore;

internal class Player
{
    private string _name = "";

    public int Score{ get; set; }

    // Default constructor - set to private as it is not meant to be called
    // except by other constructors
    private Player()
    {
        Score = 0;
    }
    
    public Player(string name) : this()
    {
        _name = name;
    }

    public Player(int playerNumber) : this()
    {
        Console.Write($"Please enter the name for Player {playerNumber}: ");
        while(true)
        {
            try{
                _name = Console.ReadLine() ?? "";
                if(_name == "")
                    throw new PlayerUnnamedException();
                break;
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
}