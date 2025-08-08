using System.Collections.Immutable;

namespace Hnefatafl.Domain
{
    public sealed class BoardState
    {
        private readonly ImmutableDictionary<Coordinate, Piece> _map;

        private BoardState(ImmutableDictionary<Coordinate, Piece> map)
        {
            _map = map;
        }

        public static BoardState Empty() => 
            new BoardState(ImmutableDictionary<Coordinate, Piece>.Empty);

        public bool TryGetPiece(Coordinate c, out Piece p) => 
            _map.TryGetValue(c, out p);

        public BoardState WithPiece(Coordinate c, Piece p) => 
            new BoardState(_map.SetItem(c, p));

        public BoardState Without(Coordinate c) =>
            new BoardState(_map.Remove(c));
    }
}
