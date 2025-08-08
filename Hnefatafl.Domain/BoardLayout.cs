using System.Collections.Immutable;

namespace Hnefatafl.Domain;

public static class BoardLayout
{
    public const int Size = 11;
    public static readonly Coordinate Center = new('F', 6);

    public static readonly ImmutableHashSet<Coordinate> corners =
        ImmutableHashSet.Create(
            new Coordinate('A', 1),
            new Coordinate('A', 11),
            new Coordinate('K', 1),
            new Coordinate('K', 11)
        );

    public static bool IsInside(Coordinate c) =>
        c.File is >= 'A' and <= 'K' && c.Rank is >= 1 and <= 11;

    public static bool IsThrone(Coordinate c) => c == Center;
    public static bool IsCorner(Coordinate c) => corners.Contains(c);
    public static bool IsHostile(Coordinate c) => IsThrone(c) || IsCorner(c);

    //Only the king can stop on the throne or corner spaces,
    //other pieces can only pass over the throne when it's empty.
    public static bool CanStop(PieceType type, Coordinate to) =>
        type == PieceType.King || !IsHostile(to);
}


