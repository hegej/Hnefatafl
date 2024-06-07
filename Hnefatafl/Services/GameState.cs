using Hnefatafl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Services
{
    public class GameState
    {
        public GamePiece[,] Board { get; private set; }
        public bool IsWhiteTurn { get; private set; }

        public GameState()
        {
            Board = new GamePiece[11, 11];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Board[i, j] = new GamePiece() { Type = PieceType.Empty };
                }
            }

            //King in the center
            Board[5, 5] = new GamePiece() { Type = PieceType.King };

            //The defenders around the king
            int[] defenderPositions = { 4, 5, 6 };
            foreach (int i in defenderPositions)
            {
                Board[5, i] = new GamePiece() { Type = PieceType.Defender };
                Board[i, 5] = new GamePiece() { Type = PieceType.Defender };
                Board[5, 11 - i - 1] = new GamePiece() { Type = PieceType.Defender };
                Board[11 - i - 1, 5] = new GamePiece() { Type = PieceType.Defender };
            }

            //Attacker 4 groups
            for (int i = 3; i <= 7; i++)
            {
                Board[i, 0] = new GamePiece() { Type = PieceType.Warrior };
                Board[i, 10] = new GamePiece() { Type = PieceType.Warrior };
                Board[0, i] = new GamePiece() { Type = PieceType.Warrior };
                Board[10, i] = new GamePiece() { Type = PieceType.Warrior };
            }

            IsWhiteTurn = true;
        }

        public bool IsValidMove(int startX, int startY, int endX, int endY)
        {
            //Check boundary
            if (startX < 0 || startX > 10 || startY < 0 || startY > 10 || endX < 0 || endX > 10 || endY < 0 || endY > 10)
                return false;

            //Moving to not empty cell
            if ((startX != endX && startY != endY) || (startX == endX && startY == endY))

                return false;

            //check if path is clear
            int stepX = (endX > startX) ? 1 : -1;
            int stepY = (endY > startY) ? 1 : -1;

            // Vertical
            if (startX == endX)
            {
                for (int j = startY + stepY; j != endY; j += stepY)
                    if (Board[startX, j].Type != PieceType.Empty)

                        return false;
            }

            // Horizontal
            else if (startY == endY)
            {
                for (int i = startX + stepX; i != endX; i += stepX)
                    if (Board[i, startY].Type != PieceType.Empty)

                        return false;
            }

            return true;
        }

        public bool CheckWinCondtion()
        {
            //King reach one edge of the board
            for (int i = 0; i < 11; i++)
            {
                if (Board[i, 0].Type == PieceType.King || Board[i, 10].Type == PieceType.King ||
                    Board[0, i].Type == PieceType.King || Board[10, i].Type == PieceType.King)

                    return true;
            }

            return false;
        }
    }

}

