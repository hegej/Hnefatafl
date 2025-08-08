
namespace Hnefatafl.Domain;
    
public readonly record struct Move(Coordinate From, Coordinate To)
{
    public override string ToString() => $"{From}->{To}";
}
