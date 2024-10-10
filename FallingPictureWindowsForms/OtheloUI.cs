using System;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    public class OthelloGameUI : Form
    {
        private Button[,] buttonGrid;
        private Label statusLabel;
        private int boardSize;

        public OthelloGameUI(int boardSize)
        {
            this.boardSize = boardSize;
            InitializeGameForm();
        }
        private void InitializeGameForm()
        {
            this.Text = "Othello Game";
            this.Size = new Size(400, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            buttonGrid = new Button[boardSize, boardSize];
            int buttonSize = 40;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(j * buttonSize, i * buttonSize),
                        Tag = new Point(i, j)
                    };
                    buttonGrid[i, j] = btn;
                    this.Controls.Add(btn);
                }
            }

            statusLabel = new Label
            {
                Location = new Point(10, boardSize * buttonSize + 10),
                Size = new Size(this.Width - 20, 30),
                Text = "Game Status"
            };
            this.Controls.Add(statusLabel);
        }
        
        public void SetCellClickHandler(EventHandler handler)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    buttonGrid[i, j].Click += handler;
                }
            }
        }

        public void UpdateBoardDisplay(char[,] board, bool[,] validMoves)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    buttonGrid[i, j].Text = board[i, j].ToString();
                    buttonGrid[i, j].Enabled = validMoves[i, j];
                }
            }
        }

        public void UpdateStatusLabel(string status)
        {
            statusLabel.Text = status;
        }

        public void ShowGameResult(string message)
        {
            MessageBox.Show(message, "Game Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        public void Showdialog()
        {
            this.ShowDialog();
        }
    }
}