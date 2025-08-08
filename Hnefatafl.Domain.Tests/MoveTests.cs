using Hnefatafl.Domain;
using Xunit;

public class MoveTests
{
    [Fact]
    public void ToString_prints_nicely()
    {
        var m = new Move(new('E', 6), new('E', 8));
        Assert.Equal("E6->E8", m.ToString());
    }
}

