using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Controls the game logic for Othello.
    /// </summary>
    public class GameController
    {
        private Board m_Board;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;
        private OthelloGameUI m_GameUI;
        private Random m_Random;

        /// <summary>
        /// Initializes a new instance of the GameController class.
        /// </summary>
        public GameController(Player i_Player1, Player i_Player2, int i_BoardSize)
        {
            this.m_Board = new Board(i_BoardSize);
            this.r_Player1 = i_Player1;
            this.r_Player2 = i_Player2;
            this.m_CurrentPlayer = i_Player1;
            this.m_GameUI = new OthelloGameUI(i_BoardSize);
            this.m_Random = new Random();
        }

        /// <summary>
        /// Starts the Othello game.
        /// </summary>
        public void startGame()
        {
            m_GameUI.setCellClickHandler(cellButton_Click);
            initializeBoard();
            updateBoardDisplay();
            updateStatusLabel();
            while (true)
            {
                DialogResult result = m_GameUI.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    break;
                }
                resetGame();
            }
        }

        /// <summary>
        /// Resets the game to its initial state.
        /// </summary>
        private void resetGame()
        {
            m_Board = new Board(m_Board.Size);
            m_CurrentPlayer = r_Player1;
            initializeBoard();
            updateBoardDisplay();
            updateStatusLabel();
        }

        /// <summary>
        /// Initializes the game board with starting positions.
        /// </summary>
        private void initializeBoard()
        {
            int mid = m_Board.Size / 2;
            m_Board.setSpecificCell(mid - 1, mid - 1, r_Player1.XOSymbol);
            m_Board.setSpecificCell(mid, mid, r_Player1.XOSymbol);
            m_Board.setSpecificCell(mid - 1, mid, r_Player2.XOSymbol);
            m_Board.setSpecificCell(mid, mid - 1, r_Player2.XOSymbol);
        }

        /// <summary>
        /// Updates the display of the game board.
        /// </summary>
        private void updateBoardDisplay()
        {
            char[,] boardState = new char[m_Board.Size, m_Board.Size];
            bool[,] validMoves = new bool[m_Board.Size, m_Board.Size];

            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    boardState[i, j] = m_Board.getSpecificCell(i, j);
                    validMoves[i, j] = m_Board.validateMove(m_CurrentPlayer, i, j);
                }
            }

            m_GameUI.updateBoardDisplay(boardState, validMoves);
        }

        /// <summary>
        /// Handles the click event on a cell button.
        /// </summary>
        private void cellButton_Click(object sender, EventArgs e)
        {
            if (m_CurrentPlayer.r_IsComputer) return;

            if (sender is Button clickedButton)
            {
                if (clickedButton.Tag is Point location)
                {
                    int row = location.X;
                    int col = location.Y;

                    if (m_Board.validateMove(m_CurrentPlayer, row, col))
                    {
                        makeMove(row, col);
                    }
                }
            }
        }

        /// <summary>
        /// Makes a move on the board and updates the game state.
        /// </summary>
        private void makeMove(int i_Row, int i_Col)
        {
            try
            {
                m_Board.setSpecificCell(i_Row, i_Col, m_CurrentPlayer.XOSymbol);
                m_Board.flipPieces(m_CurrentPlayer, i_Row, i_Col);

                switchTurn();
                updateBoardDisplay();
                updateStatusLabel();

                if (checkGameOver())
                {
                    displayGameResult();
                }
                else if (!hasValidMoves())
                {
                    switchTurn();
                    updateBoardDisplay();
                    updateStatusLabel();

                    if (m_CurrentPlayer.r_IsComputer)
                    {
                        makeComputerMove();
                    }
                }
                else if (m_CurrentPlayer.r_IsComputer)
                {
                    makeComputerMove();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while making a move: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Makes a move for the computer player.
        /// </summary>
        private void makeComputerMove()
        {
            try
            {
                List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();

                for (int i = 0; i < m_Board.Size; i++)
                {
                    for (int j = 0; j < m_Board.Size; j++)
                    {
                        if (m_Board.validateMove(m_CurrentPlayer, i, j))
                        {
                            validMoves.Add(new Tuple<int, int>(i, j));
                        }
                    }
                }

                if (validMoves.Count > 0)
                {
                    var move = validMoves[m_Random.Next(validMoves.Count)];
                    makeMove(move.Item1, move.Item2);
                }
                else
                {
                    switchTurn();
                    updateStatusLabel();
                    if (!hasValidMoves())
                    {
                        displayGameResult();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during the computer's move: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the status label with the current player's turn.
        /// </summary>
        private void updateStatusLabel()
        {
            m_GameUI.updateStatusLabel($"{m_CurrentPlayer.Name}'s turn ({m_CurrentPlayer.XOSymbol})");
        }

        /// <summary>
        /// Switches the turn to the other player.
        /// </summary>
        private void switchTurn()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }

        /// <summary>
        /// Checks if the game is over.
        /// </summary>
        private bool checkGameOver()
        {
            return !hasValidMoves() && !hasValidMovesForOtherPlayer();
        }

        /// <summary>
        /// Checks if the current player has any valid moves.
        /// </summary>
        private bool hasValidMoves()
        {
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_Board.validateMove(m_CurrentPlayer, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the other player has any valid moves.
        /// </summary>
        private bool hasValidMovesForOtherPlayer()
        {
            Player otherPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_Board.validateMove(otherPlayer, i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Displays the game result and prompts for a new game.
        /// </summary>
        private void displayGameResult()
        {
            int scorePlayer1 = calculateScore(r_Player1);
            int scorePlayer2 = calculateScore(r_Player2);

            string message = $"Game Over!\n\n{r_Player1.Name}: {scorePlayer1} points\n{r_Player2.Name}: {scorePlayer2} points\n\n";

            if (scorePlayer1 > scorePlayer2)
            {
                message += $"{r_Player1.Name} wins!";
            }
            else if (scorePlayer2 > scorePlayer1)
            {
                message += $"{r_Player2.Name} wins!";
            }
            else
            {
                message += "It's a tie!";
            }

            DialogResult result = m_GameUI.showGameResult(message);
            if (result == DialogResult.Retry)
            {
                resetGame();
            }
            else
            {
                m_GameUI.DialogResult = DialogResult.Cancel;
                m_GameUI.Close();
            }
        }

        /// <summary>
        /// Calculates the score for a given player.
        /// </summary>
        private int calculateScore(Player i_Player)
        {
            int score = 0;
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_Board.getSpecificCell(i, j) == i_Player.XOSymbol)
                    {
                        score++;
                    }
                }
            }
            return score;
        }
    }
}