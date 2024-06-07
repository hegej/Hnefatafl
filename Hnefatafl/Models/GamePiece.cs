using System;
using Microsoft.Maui.Graphics;

namespace Hnefatafl.Models
{
    public enum PieceType
    {
        Empty,
        Defender,
        Warrior,
        King
    }

    public class GamePiece
    {
        public PieceType Type { get; set; }
        private Color _pieceColor;

        public Color PieceColor
        {
            get { return _pieceColor; }
            set { _pieceColor = value; }
        }

        public GamePiece()
        {

            SetDefaultColor();
        }

        private void SetDefaultColor()
        {
            switch (Type)
            {
                case PieceType.Warrior:
                    _pieceColor = Colors.Red;  // Example color for warriors
                    break;
                case PieceType.Defender:
                    _pieceColor = Colors.Blue; // Example color for defenders
                    break;
                case PieceType.King:
                    _pieceColor = Colors.Gold; // Example color for the king
                    break;
                case PieceType.Empty:
                default:
                    _pieceColor = Colors.Transparent; // Empty or default
                    break;
            }
        }
    }
}
