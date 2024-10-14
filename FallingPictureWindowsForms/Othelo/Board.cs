using System;
using System.Collections.Generic;
using System.Linq;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents the Othello game board.
    /// </summary>
    internal class Board
    {
        private readonly Cell[,] r_BoardMatrix;

        /// <summary>
        /// Gets the size of the board.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Initializes a new instance of the Board class.
        /// </summary>
        /// <param name="i_Size">The size of the board.</param>
        public Board(int i_Size = 8)
        {
            Size = i_Size;
            r_BoardMatrix = new Cell[i_Size, i_Size];
            initializeBoard();
        }

        /// <summary>
        /// Gets the value of a specific cell on the board.
        /// </summary>
        public char getSpecificCell(int i_Row, int i_Col)
        {
            if (i_Row < 0 || i_Row >= Size || i_Col < 0 || i_Col >= Size)
            {
                throw new IndexOutOfRangeException("Row or Column is outside the bounds of the board");
            }
            return this.r_BoardMatrix[i_Row, i_Col].InputCell;
        }

        /// <summary>
        /// Sets the value of a specific cell on the board.
        /// </summary>
        public void setSpecificCell(int i_Row, int i_Col, char i_PlayerValue)
        {
            this.r_BoardMatrix[i_Row, i_Col].InputCell = i_PlayerValue;
        }

        /// <summary>
        /// Initializes the board with starting positions.
        /// </summary>
        private void initializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    r_BoardMatrix[i, j] = new Cell();
                }
            }

            int mid = Size / 2;
            r_BoardMatrix[mid - 1, mid - 1].updateCellStatus("White", 'O');
            r_BoardMatrix[mid, mid].updateCellStatus("White", 'O');
            r_BoardMatrix[mid, mid - 1].updateCellStatus("Black", 'X');
            r_BoardMatrix[mid - 1, mid].updateCellStatus("Black", 'X');
        }

        /// <summary>
        /// Validates if a move is legal for the current player.
        /// </summary>
        public bool validateMove(Player i_Current, int i_Row, int i_Col)
        {
            if (i_Row < 0 || i_Col < 0 || i_Row >= Size || i_Col >= Size)
            {
                return false;
            }

            if (getSpecificCell(i_Row, i_Col) != ' ')
            {
                return false;
            }

            return checkAllDirections(i_Current, i_Row, i_Col);
        }

        /// <summary>
        /// Checks all directions for a valid move.
        /// </summary>
        private bool checkAllDirections(Player i_Current, int i_Row, int i_Col)
        {
            int[] directions = { -1, 0, 1 };
            foreach (int dx in directions)
            {
                foreach (int dy in directions)
                {
                    if (dx == 0 && dy == 0) continue;
                    if (checkDirection(i_Current, i_Row, i_Col, dx, dy))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks a specific direction for a valid move.
        /// </summary>
        private bool checkDirection(Player i_Current, int i_Row, int i_Col, int i_Dx, int i_Dy)
        {
            int x = i_Row + i_Dx;
            int y = i_Col + i_Dy;
            bool foundOpponent = false;

            while (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                char cell = getSpecificCell(x, y);
                if (cell == ' ') return false;
                if (cell == i_Current.XOSymbol)
                {
                    return foundOpponent;
                }
                foundOpponent = true;
                x += i_Dx;
                y += i_Dy;
            }

            return false;
        }

        /// <summary>
        /// Flips pieces after a valid move.
        /// </summary>
        public void flipPieces(Player i_Current, int i_Row, int i_Col)
        {
            int[] directions = { -1, 0, 1 };
            foreach (int dx in directions)
            {
                foreach (int dy in directions)
                {
                    if (dx == 0 && dy == 0) continue;
                    flipDirection(i_Current, i_Row, i_Col, dx, dy);
                }
            }
        }

        /// <summary>
        /// Flips pieces in a specific direction.
        /// </summary>
        private void flipDirection(Player i_Current, int i_Row, int i_Col, int i_Dx, int i_Dy)
        {
            int x = i_Row + i_Dx;
            int y = i_Col + i_Dy;
            List<Tuple<int, int>> toFlip = new List<Tuple<int, int>>();

            while (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                char cell = getSpecificCell(x, y);
                if (cell == ' ') return;
                if (cell == i_Current.XOSymbol)
                {
                    foreach (var pos in toFlip)
                    {
                        setSpecificCell(pos.Item1, pos.Item2, i_Current.XOSymbol);
                    }
                    return;
                }
                toFlip.Add(new Tuple<int, int>(x, y));
                x += i_Dx;
                y += i_Dy;
            }
        }
    }
}