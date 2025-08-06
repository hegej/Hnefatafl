using Hnefatafl.Domain;
using Xunit;

namespace Hnefatafl.Domain.Tests;

public class CoordinateTests
{
    [Theory]
    [InlineData("A1", 'A', 1)]
    [InlineData("K11", 'K', 11)]

    public void Parse_valid_coordinates(string txt, char f, int r)
    {
        var c = Coordinate.Parse(txt);
        Assert.Equal((f, r), (c.File, c.Rank));
    }

}
