namespace ThreeOrMore;

internal class Program
{
    static void Main(string[] args)
    {
        var die = new Die(6);

        var counts = Enumerable.Repeat(0, 7).ToList();

        for(int i = 0; i < 10000; i++)
            counts[die.Roll()] ++;

        for(int i = 1; i < counts.Count; i++)
            Console.WriteLine($" {i} appears {counts[i]} times");
    }
}