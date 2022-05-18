namespace ThreeOrMore;

internal class Player
{
    private string _name = "";

    public int Score{ get; set; }
    public string Name{ get => _name; }

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
}