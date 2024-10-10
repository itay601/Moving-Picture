using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    public class GameController
    {
        private Board board;
        private readonly Player player1;
        private readonly Player player2;
        private Player currentPlayer;
        private OthelloGameUI gameUI;

        public GameController(Player player1, Player player2, int boardSize)
        {
            this.board = new Board(boardSize);
            this.player1 = player1;
            this.player2 = player2;
            this.currentPlayer = player1;
            this.gameUI = new OthelloGameUI(boardSize);
        }

        public void StartGame()
        {
            gameUI.SetCellClickHandler(CellButton_Click);
            InitializeBoard();
            UpdateBoardDisplay();
            UpdateStatusLabel();
            gameUI.ShowDialog();
        }

        private void InitializeBoard()
        {
            int mid = board.Size / 2;
            board.SetSpecificCell(mid - 1, mid - 1, player1.XOSymbole);
            board.SetSpecificCell(mid, mid, player1.XOSymbole);
            board.SetSpecificCell(mid - 1, mid, player2.XOSymbole);
            board.SetSpecificCell(mid, mid - 1, player2.XOSymbole);
        }

        private void UpdateBoardDisplay()
        {
            char[,] boardState = new char[board.Size, board.Size];
            bool[,] validMoves = new bool[board.Size, board.Size];

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    boardState[i, j] = board.GetSpecificCell(i, j);
                    validMoves[i, j] = board.ValidateMove(currentPlayer, i, j);
                }
            }

            gameUI.UpdateBoardDisplay(boardState, validMoves);
        }

        private void CellButton_Click(object sender, EventArgs e)
        {
            if (currentPlayer.m_isComputer) return;

            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;
            int row = location.X;
            int col = location.Y;

            if (board.ValidateMove(currentPlayer, row, col))
            {
                MakeMove(row, col);
            }
        }

        private void MakeMove(int row, int col)
        {
            board.SetSpecificCell(row, col, currentPlayer.XOSymbole);
            board.FlipPieces(currentPlayer, row, col);

            SwitchTurn();
            UpdateBoardDisplay();
            UpdateStatusLabel();

            if (CheckGameOver())
            {
                DisplayGameResult();
            }
            else if (!HasValidMoves())
            {
                SwitchTurn();
                UpdateBoardDisplay();
                UpdateStatusLabel();

                if (currentPlayer.m_isComputer)
                {
                    MakeComputerMove();
                }
            }
            else if (currentPlayer.m_isComputer)
            {
                MakeComputerMove();
            }
        }
        private void MakeComputerMove()
        {
            Random rand = new Random();
            List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.ValidateMove(currentPlayer, i, j))
                    {
                        validMoves.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            if (validMoves.Count > 0)
            {
                var move = validMoves[rand.Next(validMoves.Count)];
                MakeMove(move.Item1, move.Item2);
            }
            else
            {
                SwitchTurn();
                UpdateStatusLabel();
                if (!HasValidMoves())
                {
                    DisplayGameResult();
                }
            }
        }

        private void UpdateStatusLabel()
        {
            gameUI.UpdateStatusLabel($"{currentPlayer.Name}'s turn ({currentPlayer.XOSymbole})");
        }

        private void SwitchTurn()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        private bool CheckGameOver()
        {
            return !HasValidMoves() && !HasValidMovesForOtherPlayer();
        }

        private bool HasValidMoves()
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.ValidateMove(currentPlayer, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool HasValidMovesForOtherPlayer()
        {
            Player otherPlayer = (currentPlayer == player1) ? player2 : player1;
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.ValidateMove(otherPlayer, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DisplayGameResult()
        {
            int scorePlayer1 = CalculateScore(player1);
            int scorePlayer2 = CalculateScore(player2);

            string message = $"Game Over!\n\n{player1.Name}: {scorePlayer1} points\n{player2.Name}: {scorePlayer2} points\n\n";

            if (scorePlayer1 > scorePlayer2)
            {
                message += $"{player1.Name} wins!";
            }
            else if (scorePlayer2 > scorePlayer1)
            {
                message += $"{player2.Name} wins!";
            }
            else
            {
                message += "It's a tie!";
            }

            gameUI.ShowGameResult(message);
        }

        private int CalculateScore(Player player)
        {
            int score = 0;
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (board.GetSpecificCell(i, j) == player.XOSymbole)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
    }
}