
namespace Hnefatafl.Domain;

public static class BoardSetup
{
    public static BoardState CreateStandardBoard()
    {
        var board = BoardState.Empty()
        .WithPiece(BoardLayout.Center, new Piece(PieceType.King));

        var d = new Piece(PieceType.Defender);
        var defenders = new[]
        {
            new Coordinate('F', 4), new('F', 5), new('F', 7), new('F', 8),
            new Coordinate('D', 6), new('E', 6), new('G', 6), new('H', 6),
            new Coordinate('E', 5), new('E', 7), new('G', 5), new('G', 7),
        };

        foreach (var c in defenders) board = board.WithPiece(c, d);

        var a = new Piece(PieceType.Attacker);
        var attackers = new[]
        {
            new Coordinate('D', 1),  new('E', 1),  new('F', 1),  new('G', 1),  new('H', 1),  new('F', 2),
            new Coordinate('D', 11), new('E', 11), new('F', 11), new('G', 11), new('H', 11), new('F', 10), 
            new Coordinate('A', 4),  new('A', 5),  new('A', 6),  new('A', 7),  new('A', 8),  new('B', 6), 
            new Coordinate('K', 4),  new('K', 5),  new('K', 6),  new('K', 7),  new('K', 8),  new('J', 6),
        };

        foreach (var c in attackers) board = board.WithPiece(c, a);

        return board;
    }
}