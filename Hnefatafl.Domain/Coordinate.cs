namespace Hnefatafl.Domain;

// A grid/space on the baord A-K, 1-11, is a unchangeable value (char + int) 

public readonly record struct Coordinate(char File, int Rank)
{
    public override string ToString() => $"{File}{Rank}";

    public static Coordinate Parse (string s)
    {
        if (s.Length is < 2 or > 3) throw new FormatException("Expected E6");
        var file = char.ToUpperInvariant(s[0]); //normalize
        var rank = int.Parse(s[1..]); // slice from index 1 til end
        return new Coordinate(file, rank) ;
    }
}
