using Hnefatafl.Domain;
using Xunit;

public class BoardStateTests
{

    [Fact]
    public void WithPiece_return_new_instance_while_keep_the_original()
    {
        var b1 = BoardState.Empty();
        var b2 = b1.WithPiece(new('E', 6), new Piece(PieceType.King));

        Assert.False(b1.TryGetPiece(new('E', 6), out _)); //uendret
        Assert.True(b2.TryGetPiece(new('E', 6), out var p) && p.Type == PieceType.King);
    }

}
