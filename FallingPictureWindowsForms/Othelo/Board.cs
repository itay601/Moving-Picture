using System;
using System.Collections.Generic;
using System.Linq;

namespace FallingPictureWindowsForms
{
    internal class Board
    {
        private readonly Cell[,] m_boardMatrix;
        public int Size { get; }

        public Board(int i_size = 8)
        {
            Size = i_size;
            m_boardMatrix = new Cell[i_size, i_size];
            InitializeBoard();
        }

        public char GetSpecificCell(int i_row, int i_col)
        {
            if (i_row < 0 || i_row >= Size || i_col < 0 || i_col >= Size)
            {
                throw new IndexOutOfRangeException("Row or Column is outside the bounds of the board");
            }
            return this.m_boardMatrix[i_row, i_col].m_InputCell;
        }

        public void SetSpecificCell(int i_row, int i_col, char playerValue)
        {
            this.m_boardMatrix[i_row, i_col].m_InputCell = playerValue;
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    m_boardMatrix[i, j] = new Cell();
                }
            }

            int mid = Size / 2;
            m_boardMatrix[mid - 1, mid - 1].UpdateCellStatus("White", 'O');
            m_boardMatrix[mid, mid].UpdateCellStatus("White", 'O');
            m_boardMatrix[mid, mid - 1].UpdateCellStatus("Black", 'X');
            m_boardMatrix[mid - 1, mid].UpdateCellStatus("Black", 'X');
        }

        public bool ValidateMove(Player i_current, int i_row, int i_col)
        {
            if (i_row < 0 || i_col < 0 || i_row >= Size || i_col >= Size)
            {
                return false;
            }

            if (GetSpecificCell(i_row, i_col) != ' ')
            {
                return false;
            }

            return CheckAllDirections(i_current, i_row, i_col);
        }

        private bool CheckAllDirections(Player i_current, int i_row, int i_col)
        {
            int[] directions = { -1, 0, 1 };
            foreach (int dx in directions)
            {
                foreach (int dy in directions)
                {
                    if (dx == 0 && dy == 0) continue;
                    if (CheckDirection(i_current, i_row, i_col, dx, dy))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckDirection(Player i_current, int i_row, int i_col, int dx, int dy)
        {
            int x = i_row + dx;
            int y = i_col + dy;
            bool foundOpponent = false;

            while (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                char cell = GetSpecificCell(x, y);
                if (cell == ' ') return false;
                if (cell == i_current.XOSymbole)
                {
                    return foundOpponent;
                }
                foundOpponent = true;
                x += dx;
                y += dy;
            }

            return false;
        }

        public void FlipPieces(Player i_current, int i_row, int i_col)
        {
            int[] directions = { -1, 0, 1 };
            foreach (int dx in directions)
            {
                foreach (int dy in directions)
                {
                    if (dx == 0 && dy == 0) continue;
                    FlipDirection(i_current, i_row, i_col, dx, dy);
                }
            }
        }

        private void FlipDirection(Player i_current, int i_row, int i_col, int dx, int dy)
        {
            int x = i_row + dx;
            int y = i_col + dy;
            List<Tuple<int, int>> toFlip = new List<Tuple<int, int>>();

            while (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                char cell = GetSpecificCell(x, y);
                if (cell == ' ') return;
                if (cell == i_current.XOSymbole)
                {
                    foreach (var pos in toFlip)
                    {
                        SetSpecificCell(pos.Item1, pos.Item2, i_current.XOSymbole);
                    }
                    return;
                }
                toFlip.Add(new Tuple<int, int>(x, y));
                x += dx;
                y += dy;
            }
        }

        public void DisplayBoard()
        {
            Console.WriteLine("    " + string.Join("   ", Enumerable.Range(0, Size).Select(i => (char)('A' + i))));
            Console.WriteLine("  " + new string('=', Size * 4 + 1));

            for (int i = 0; i < Size; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < Size; j++)
                {
                    Console.Write($"| {GetSpecificCell(i, j)} ");
                }
                Console.WriteLine("|");
                Console.WriteLine("  " + new string('=', Size * 4 + 1));
            }
        }
    }
}