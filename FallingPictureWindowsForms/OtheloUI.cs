using System;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents the main game UI for Othello.
    /// </summary>
    public class OthelloGameUI : Form
    {
        private Button[,] m_ButtonGrid;
        private Label m_StatusLabel;
        private int m_BoardSize;
        private const int k_ButtonSize = 40;
        private const int k_Padding = 10;

        /// <summary>
        /// Initializes a new instance of the OthelloGameUI class.
        /// </summary>
        /// <param name="i_BoardSize">The size of the Othello board.</param>
        public OthelloGameUI(int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            initializeGameForm();
        }

        /// <summary>
        /// Initializes and sets up all the UI components of the game form.
        /// </summary>
        private void initializeGameForm()
        {
            this.Text = "Othello Game";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            int formWidth = (m_BoardSize * k_ButtonSize) + (2 * k_Padding);
            int formHeight = (m_BoardSize * k_ButtonSize) + (3 * k_Padding) + 30; // Extra space for status label
            this.ClientSize = new Size(formWidth, formHeight);

            m_ButtonGrid = new Button[m_BoardSize, m_BoardSize];
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(k_ButtonSize, k_ButtonSize),
                        Location = new Point(k_Padding + (j * k_ButtonSize), k_Padding + (i * k_ButtonSize)),
                        Tag = new Point(i, j),
                        Font = new Font(Font.FontFamily, 14, FontStyle.Bold)
                    };
                    m_ButtonGrid[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }

            m_StatusLabel = new Label
            {
                Location = new Point(k_Padding, k_Padding + (m_BoardSize * k_ButtonSize)),
                Size = new Size(formWidth - (2 * k_Padding), 30),
                Text = "Game Status"
            };
            this.Controls.Add(m_StatusLabel);
        }

        /// <summary>
        /// Sets the click event handler for all cell buttons.
        /// </summary>
        /// <param name="i_Handler">The event handler to be set.</param>
        public void setCellClickHandler(EventHandler i_Handler)
        {
            if (i_Handler != null)
            {
                for (int i = 0; i < m_BoardSize; i++)
                {
                    for (int j = 0; j < m_BoardSize; j++)
                    {
                        m_ButtonGrid[i, j].Click += i_Handler;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the display of the game board.
        /// </summary>
        /// <param name="i_Board">The current state of the board.</param>
        /// <param name="i_ValidMoves">The valid moves for the current player.</param>
        public void updateBoardDisplay(char[,] i_Board, bool[,] i_ValidMoves)
        {
            if (i_Board != null && i_ValidMoves != null &&
                i_Board.GetLength(0) == m_BoardSize && i_Board.GetLength(1) == m_BoardSize &&
                i_ValidMoves.GetLength(0) == m_BoardSize && i_ValidMoves.GetLength(1) == m_BoardSize)
            {
                for (int i = 0; i < m_BoardSize; i++)
                {
                    for (int j = 0; j < m_BoardSize; j++)
                    {
                        m_ButtonGrid[i, j].Text = i_Board[i, j].ToString();
                        m_ButtonGrid[i, j].Enabled = i_ValidMoves[i, j];
                        m_ButtonGrid[i, j].BackColor = i_ValidMoves[i, j] ? Color.LightGreen : SystemColors.Control;
                        if (i_Board[i, j] == 'X')
                        {
                            m_ButtonGrid[i, j].ForeColor = Color.Black;
                            m_ButtonGrid[i, j].BackColor = Color.White;
                        }
                        else if (i_Board[i, j] == 'O')
                        {
                            m_ButtonGrid[i, j].ForeColor = Color.White;
                            m_ButtonGrid[i, j].BackColor = Color.Black;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the status label with the current game status.
        /// </summary>
        /// <param name="i_Status">The status message to be displayed.</param>
        public void updateStatusLabel(string i_Status)
        {
            this.Text = i_Status;
            m_StatusLabel.Text = i_Status;
        }

        /// <summary>
        /// Displays the game result and prompts for a new game.
        /// </summary>
        /// <param name="i_Message">The result message to be displayed.</param>
        /// <returns>The user's choice to retry or cancel.</returns>
        public DialogResult showGameResult(string i_Message)
        {
            return MessageBox.Show(i_Message, "Game Result", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
        }
    }
}