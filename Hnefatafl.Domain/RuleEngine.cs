
using System.Diagnostics.Contracts;

namespace Hnefatafl.Domain;

/* Rules to validate the movements (no capture)
     *  - From != To
     *  - Positions is on the board
     *  - Check that piece stands on the From space
     *  - Movement is orthogonal : Same lane or rank
     *  - Destination is empty
     *  - Only KING can stop on throne of corners
     *  - Free lane to move FROM to TO (can't pass other pieces, and only pass throne if empty)
    */

public sealed class RuleEngine
{
    public bool ValidateMove(BoardState state, Move move, out string? reason)
    {
        reason = null;

        if (move.From == move.To)
        {
            reason = "From == To";
            return false;
        }

        if (!BoardLayout.IsInside(move.From) || !BoardLayout.IsInside(move.To))
        {
            reason = "Outside board";
            return false;
        }

        var sameFile = move.From.File == move.To.File;
        var sameRank = move.From.Rank == move.To.Rank;
        if (!(sameFile || sameRank))
        {
            reason = "Only orthogonal moves are allowed";
            return false;
        }

        if (state.TryGetPiece(move.To, out _))
        {
            reason = "Must stop on an empty space";
            return false;
        }

        if (!state.TryGetPiece(move.From, out var piece))
        {
            reason = "No piece at From";
            return false;
        }

        if (!BoardLayout.CanStop(piece.Type, move.To))
        {
            reason = "Only King can stop on throne and corners";
            return false;
        }

        if (!PathIsClear(state, move))
        {
            reason = "Path is blocked";
            return false;
        }

        return true;
    }

    public BoardState ApplyMove(BoardState state, Move move) =>
        state.TryGetPiece(move.From, out var piece)
        ? state.Without(move.From).WithPiece(move.To, piece)
        : throw new InvalidOperationException("No piece to move.");


    // check for vertical or horizontal
    private static bool PathIsClear(BoardState state, Move m) =>
        m.From.File == m.To.File ? FilePathClear(state, m) : RankPathClear(state, m);

    // Vertical check
    private static bool FilePathClear(BoardState state, Move m)
    {
        var f = m.From.File;
        var step = m.To.Rank > m.From.Rank ? 1 : -1;
        for (int r = m.From.Rank + step; r != m.To.Rank; r += step)
            if (state.TryGetPiece(new(f, r), out _)) return false;
        return true;
    }

    // Horizontal check
    private static bool RankPathClear(BoardState state, Move m)
    {
        var r = m.From.Rank;
        var step = m.To.File > m.From.File ? 1 : -1;
        for (char f = (char)(m.From.File + step); f != m.To.File; f = (char)(f + step))
            if (state.TryGetPiece(new(f, r), out _)) return false;
        return true;
    }
}
