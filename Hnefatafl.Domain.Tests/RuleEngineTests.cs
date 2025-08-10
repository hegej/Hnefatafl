using Hnefatafl.Domain;

public class RuleEngineTests
{
    static BoardState BoardWith(params (string pos, PieceType t)[] pieces)
    {
        var b = BoardState.Empty();
        foreach (var (pos, t) in pieces)
            b = b.WithPiece(Coordinate.Parse(pos), new Piece(t));
        return b;
    }

    [Fact]
    public void Cannot_move_diagonally()
    {
        var board = BoardWith(("E6", PieceType.Defender));
        var re = new RuleEngine();

        var ok = re.ValidateMove(board, new(new('E', 6), new('F', 7)), out var reason);
        Assert.False(ok);
        Assert.Equal("Only orthogonal moves are allowed", reason);
    }

    [Fact]
    public void Path_must_be_clear()
    {
        var board = BoardWith(("E6", PieceType.Defender), ("E7", PieceType.Attacker));
        var re = new RuleEngine();

        var ok = re.ValidateMove(board, new(new('E', 6), new('E', 9)), out var reason);
        Assert.False(ok);
        Assert.Equal("Path is blocked", reason);
    }

    [Fact]
    public void Destination_must_be_empty()
    {
        var board = BoardWith(("E6", PieceType.Defender), ("E9", PieceType.Attacker));
        var re = new RuleEngine();

        var ok = re.ValidateMove(board, new(new('E', 6), new('E', 9)), out var reason);
        Assert.False(ok);
        Assert.Equal("Must stop on an empty space", reason);
    }

    [Fact]
    public void Non_king_cannot_stop_on_throne_even_if_path_is_legal()
    {
        var board = BoardWith(("E6", PieceType.Defender));
        var re = new RuleEngine();

        var ok = re.ValidateMove(board, new(new('E', 6), BoardLayout.Center), out var reason);
        Assert.False(ok);
        Assert.Equal("Only King can stop on throne and corners", reason);
    }

    [Fact]
    public void King_can_stop_on_corner_when_path_is_clear_and_move_is_orthogonal()
    {
        var board = BoardWith(("A6", PieceType.King));
        var re = new RuleEngine();

        var m = new Move(new('A', 6), new('A', 1));
        Assert.True(re.ValidateMove(board, m, out var reason), reason);

        var next = re.ApplyMove(board, m);
        Assert.True(next.TryGetPiece(new('A', 1), out var p) && p.Type == PieceType.King);
        Assert.False(next.TryGetPiece(new('A', 6), out _));
    }

    [Fact]
    public void ApplyMove_moves_piece_and_keeps_the_original_immutable()
    {
        var board = BoardWith(("E6", PieceType.Defender));
        var re = new RuleEngine();

        var m = new Move(new('E', 6), new('E', 8));
        Assert.True(re.ValidateMove(board, m, out _));

        var next = re.ApplyMove(board, m);
        Assert.True(next.TryGetPiece(new('E', 8), out var p) && p.Type == PieceType.Defender);
        Assert.False(board.TryGetPiece(new('E', 8), out _));
    }
}