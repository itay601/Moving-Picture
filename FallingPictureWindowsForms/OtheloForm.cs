using System;
using System.Drawing;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents the setup form for the Othello game.
    /// </summary>
    public partial class OthelloForm : Form
    {
        private TextBox m_TxtFirstPlayerName;
        private TextBox m_TxtSecondPlayerName;
        private ComboBox m_CmbBoardSize;
        private RadioButton m_RadSecondPlayer;
        private RadioButton m_RadComputer;
        private Button m_BtnStart;

        public OthelloForm()
        {
            initializeComponent();
        }

        /// <summary>
        /// Initializes and sets up all the UI components of the form.
        /// </summary>
        private void initializeComponent()
        {
            this.m_TxtFirstPlayerName = new TextBox();
            this.m_TxtSecondPlayerName = new TextBox();
            this.m_CmbBoardSize = new ComboBox();
            this.m_RadSecondPlayer = new RadioButton();
            this.m_RadComputer = new RadioButton();
            this.m_BtnStart = new Button();

            // Form setup
            this.SuspendLayout();
            this.ClientSize = new Size(300, 250);
            this.Name = "OthelloForm";
            this.Text = "Othello Setup";

            // First Player Name setup
            this.m_TxtFirstPlayerName.Location = new Point(20, 20);
            this.m_TxtFirstPlayerName.Size = new Size(200, 20);
            this.Controls.Add(this.m_TxtFirstPlayerName);
            this.Controls.Add(new Label() { Text = "First Player Name:", Location = new Point(20, 5) });

            // Second Player / Computer Selection setup
            this.m_RadSecondPlayer.Location = new Point(20, 60);
            this.m_RadSecondPlayer.Text = "Second Player";
            this.m_RadSecondPlayer.Checked = true;
            this.m_RadComputer.Location = new Point(140, 60);
            this.m_RadComputer.Text = "Computer";
            this.Controls.Add(this.m_RadSecondPlayer);
            this.Controls.Add(this.m_RadComputer);

            // Second Player Name setup
            this.m_TxtSecondPlayerName.Location = new Point(20, 100);
            this.m_TxtSecondPlayerName.Size = new Size(200, 20);
            this.Controls.Add(this.m_TxtSecondPlayerName);
            this.Controls.Add(new Label() { Text = "Second Player Name:", Location = new Point(20, 85) });

            // Board Size setup
            this.m_CmbBoardSize.Location = new Point(20, 140);
            this.m_CmbBoardSize.Size = new Size(100, 20);
            this.m_CmbBoardSize.Items.AddRange(new object[] { 6, 8 });
            this.m_CmbBoardSize.SelectedIndex = 0; // Default selection
            this.Controls.Add(this.m_CmbBoardSize);
            this.Controls.Add(new Label() { Text = "Board Size:", Location = new Point(20, 125) });

            // Start Button setup
            this.m_BtnStart.Location = new Point(100, 180);
            this.m_BtnStart.Size = new Size(100, 30);
            this.m_BtnStart.Text = "Start Game";
            this.m_BtnStart.Click += new EventHandler(this.btnStart_Click);
            this.Controls.Add(this.m_BtnStart);

            this.ResumeLayout(false);
        }

        /// <summary>
        /// Handles the click event of the Start button.
        /// Validates inputs and starts the game if all inputs are valid.
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validateInputs())
                {
                    return;
                }

                Player firstPlayer = new Player(m_TxtFirstPlayerName.Text, Color.Black, false);
                Player secondPlayer = getSecondPlayer();
                int boardSize = getBoardSize();

                GameController game = new GameController(firstPlayer, secondPlayer, boardSize);
                this.Hide(); // Hide the setup form
                game.startGame();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates user inputs before starting the game.
        /// </summary>
        /// <returns>True if all inputs are valid, false otherwise.</returns>
        private bool validateInputs()
        {
            if (string.IsNullOrWhiteSpace(m_TxtFirstPlayerName.Text))
            {
                MessageBox.Show("Please enter a name for the first player.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_RadSecondPlayer.Checked && string.IsNullOrWhiteSpace(m_TxtSecondPlayerName.Text))
            {
                MessageBox.Show("Please enter a name for the second player.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (m_CmbBoardSize.SelectedItem == null)
            {
                MessageBox.Show("Please select a board size.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates and returns the second player based on user selection.
        /// </summary>
        /// <returns>A Player object representing the second player.</returns>
        private Player getSecondPlayer()
        {
            if (m_RadSecondPlayer.Checked)
            {
                return new Player(m_TxtSecondPlayerName.Text, Color.White, false);
            }
            else
            {
                return new Player("Computer", Color.White, true);
            }
        }

        /// <summary>
        /// Retrieves the selected board size from the combo box.
        /// </summary>
        /// <returns>The selected board size.</returns>
        /// <summary>
        /// Retrieves the selected board size from the combo box.
        /// </summary>
        /// <returns>The selected board size, or 6 if parsing fails.</returns>
        private int getBoardSize()
        {
            if (m_CmbBoardSize.SelectedItem != null)
            {
                if (int.TryParse(m_CmbBoardSize.SelectedItem.ToString(), out int result))
                {
                    return result;
                }
            }
            // Default to 6 if parsing fails
            MessageBox.Show("Invalid board size selected. Using default size of 6.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return 6;
        }
    }
}