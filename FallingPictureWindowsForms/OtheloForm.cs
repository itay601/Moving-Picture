using System;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    public partial class OthelloForm : Form
    {
        private TextBox txtFirstPlayerName;
        private TextBox txtSecondPlayerName;
        private ComboBox cmbBoardSize;
        private RadioButton radSecondPlayer;
        private RadioButton radComputer;
        private Button btnStart;

        public OthelloForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtFirstPlayerName = new TextBox();
            this.txtSecondPlayerName = new TextBox();
            this.cmbBoardSize = new ComboBox();
            this.radSecondPlayer = new RadioButton();
            this.radComputer = new RadioButton();
            this.btnStart = new Button();

            // Form
            this.SuspendLayout();
            this.ClientSize = new Size(300, 250);
            this.Name = "OthelloForm";
            this.Text = "Othello Setup";

            // First Player Name
            this.txtFirstPlayerName.Location = new Point(20, 20);
            this.txtFirstPlayerName.Size = new Size(200, 20);
            this.Controls.Add(this.txtFirstPlayerName);
            this.Controls.Add(new Label() { Text = "First Player Name:", Location = new Point(20, 5) });

            // Second Player / Computer Selection
            this.radSecondPlayer.Location = new Point(20, 60);
            this.radSecondPlayer.Text = "Second Player";
            this.radSecondPlayer.Checked = true;
            this.radComputer.Location = new Point(140, 60);
            this.radComputer.Text = "Computer";
            this.Controls.Add(this.radSecondPlayer);
            this.Controls.Add(this.radComputer);

            // Second Player Name
            this.txtSecondPlayerName.Location = new Point(20, 100);
            this.txtSecondPlayerName.Size = new Size(200, 20);
            this.Controls.Add(this.txtSecondPlayerName);
            this.Controls.Add(new Label() { Text = "Second Player Name:", Location = new Point(20, 85) });

            // Board Size
            this.cmbBoardSize.Location = new Point(20, 140);
            this.cmbBoardSize.Size = new Size(100, 20);
            this.cmbBoardSize.Items.AddRange(new object[] { 6, 8 });
            this.cmbBoardSize.SelectedIndex = 0; // Default selection
            this.Controls.Add(this.cmbBoardSize);
            this.Controls.Add(new Label() { Text = "Board Size:", Location = new Point(20, 125) });

            // Start Button
            this.btnStart.Location = new Point(100, 180);
            this.btnStart.Size = new Size(100, 30);
            this.btnStart.Text = "Start Game";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.Controls.Add(this.btnStart);

            this.ResumeLayout(false);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Player firstPlayer = GetFirstPlayer();
            Player secondPlayer = GetSecondPlayer();
            int boardSize = GetBoardSize();

            GameController game = new GameController(firstPlayer, secondPlayer, boardSize);
            this.Hide(); // Hide the setup form
            game.StartGame();
            this.Close();
        }

        private Player GetFirstPlayer()
        {
            return new Player(txtFirstPlayerName.Text, Color.Red, false);
        }

        private Player GetSecondPlayer()
        {
            if (radSecondPlayer.Checked)
            {
                return new Player(txtSecondPlayerName.Text, Color.Blue, false);
            }
            else
            {
                return new Player("Computer", Color.Blue, true);
            }
        }

        private int GetBoardSize()
        {
            return cmbBoardSize.SelectedItem != null ? (int)cmbBoardSize.SelectedItem : 6;
        }
    }
}