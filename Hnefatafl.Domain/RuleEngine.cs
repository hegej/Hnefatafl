
namespace Hnefatafl.Domain;

public sealed class RuleEngine
{
    /* Rules to validate the movements (no capture)
     *  - From != To
     *  - Positions is on the board
     *  - Check that piece stands on the From space
     *  - Movement is orthogonal : Same lane or rank
     *  - Destination is empty
     *  - Only KING can stop on throne of corners
     *  - Free lane to move FROM to TO (can't pass other pieces, and only pass throne if empty)
    */
}
