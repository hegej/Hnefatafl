using Hnefatafl.Domain;

public class BoardLayoutTests
{
    [Fact]
    public void Inside_and_special_fields()
    {
        Assert.True(BoardLayout.IsInside(new('A', 1)));
        Assert.True(BoardLayout.IsCorner(new('K', 11)));
        Assert.True(BoardLayout.IsThrone(new('F', 6)));
        Assert.True(BoardLayout.IsHostile(new('A', 1)));
        Assert.False(BoardLayout.CanStop(PieceType.Defender, new('A', 1)));
        Assert.True(BoardLayout.CanStop(PieceType.King, new('A', 1)));
    }
}

