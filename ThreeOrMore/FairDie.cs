namespace ThreeOrMore;

internal class FairDie : Die
{
    public FairDie(int sides) : base(sides) {}

    public override void Roll()
    {
        _value = _random.Next(_sides) + 1;
    }
}