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
        public PieceType _type { get; set; }
        private Color _pieceColor;

        public PieceType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                SetDefaultColor(); 
            }
        }

        public Color PieceColor
        {
            get { return _pieceColor; }
            private set { _pieceColor = value; } 
        }

        public GamePiece()
        {
            Type = PieceType.Empty;
        }


        private void SetDefaultColor()
        {
            switch (_type) 
            {
                case PieceType.Warrior:
                    _pieceColor = Colors.Red; 
                    break;
                case PieceType.Defender:
                    _pieceColor = Colors.Blue;
                    break;
                case PieceType.King:
                    _pieceColor = Colors.Gold;
                    break;
                case PieceType.Empty:
                default:
                    _pieceColor = Colors.Transparent;
                    break;
            }
        }
    }
}
