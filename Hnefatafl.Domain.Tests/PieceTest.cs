using Hnefatafl.Domain;
using Xunit;

public class PieceTest
{
    [Fact]
    public void Equality_is_value_based()
    {
        var p1 = new Piece(PieceType.Defender);
        var p2 = new Piece(PieceType.Defender);
        Assert.Equal(p1, p2);
    }
}