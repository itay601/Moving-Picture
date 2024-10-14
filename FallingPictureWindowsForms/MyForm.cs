using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FallingPictureWindowsForms.Properties;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents the main form of the application.
    /// </summary>
    internal class MyForm : Form
    {
        private Button m_ButtonStart;
        private Random m_Random = new Random();
        private Timer m_PictureFallTimer = new Timer();
        private List<PictureBox> m_FallPictureBox = new List<PictureBox>();
        private Button m_ButtonLogin;
        private bool m_IsLogin = false;
        private Button m_ButtonOthelloForm;

        /// <summary>
        /// Initializes a new instance of the MyForm class.
        /// </summary>
        public MyForm()
        {
            this.formSetup();
        }

        /// <summary>
        /// Sets up the form and its controls.
        /// </summary>
        public void formSetup()
        {
            this.Text = "Start Game";
            this.Size = new Size(500, 500);
            this.Location = new Point(0, 0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.LocationChanged += myForm_LocationChanged;
            this.SizeChanged += myForm_SizeChanged;

            setupStartButton();
            setupLoginButton();
            setupOthelloButton();
        }

        private void setupStartButton()
        {
            m_ButtonStart = new Button();
            m_ButtonStart.Text = this.Text;
            m_ButtonStart.Click += buttonStart_Click;
            this.Controls.Add(m_ButtonStart);
        }

        private void setupLoginButton()
        {
            m_ButtonLogin = new Button();
            m_ButtonLogin.Text = "Login";
            m_ButtonLogin.Location = new Point(0, 30);
            m_ButtonLogin.Click += buttonLogin_Click;
            this.Controls.Add(m_ButtonLogin);
        }

        private void setupOthelloButton()
        {
            m_ButtonOthelloForm = new Button();
            m_ButtonOthelloForm.Text = "Othello Game";
            m_ButtonOthelloForm.Location = new Point(30, 60);
            m_ButtonOthelloForm.Click += buttonOthelloForm_Click;
            this.Controls.Add(m_ButtonOthelloForm);
        }

        private void buttonOthelloForm_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                OthelloForm formOthello = new OthelloForm();
                formOthello.StartPosition = FormStartPosition.CenterScreen;
                formOthello.ShowDialog();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                FormLogin formLogin = new FormLogin();
                formLogin.StartPosition = FormStartPosition.CenterScreen;
                formLogin.ShowDialog();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (sender is Button && checkLoggedIn())
            {
                createRandomButtons();
            }
        }

        private bool checkLoggedIn()
        {
            if (!m_IsLogin)
            {
                FormLogin formLogin = new FormLogin();
                formLogin.StartPosition = FormStartPosition.CenterScreen;
                formLogin.ShowDialog();
                if (formLogin.ClosedByLogin)
                {
                    if (Authentication.checkUserCredentials(formLogin.TextBoxUsername, formLogin.TextBoxPassword))
                    {
                        m_IsLogin = true;
                    }
                }
            }
            return m_IsLogin;
        }

        private void createRandomButtons()
        {
            createRandomButton();
            createRandomPictureBox();
            setupPictureFallTimer();
        }

        private void createRandomButton()
        {
            Button button = new Button();
            button.Width = 110;
            int location = m_Random.Next(this.ClientSize.Width - button.Width);
            button.Left = location;
            button.Top = location;
            button.Click += randomButton_Click;
            this.Controls.Add(button);
        }

        private void createRandomPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Width = 60;
            pictureBox.Height = 60;
            pictureBox.Left = m_Random.Next(this.ClientSize.Width - pictureBox.Width);
            pictureBox.Top = m_Random.Next(this.ClientSize.Height - pictureBox.Height);
            pictureBox.Click += pictureBox_Click;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox.Image = Resources.KaliMoto;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Controls.Add(pictureBox);
        }

        private void setupPictureFallTimer()
        {
            this.m_PictureFallTimer.Interval = 100;
            this.m_PictureFallTimer.Tick += pictureFallTimer_Tick;
        }

        private void pictureFallTimer_Tick(object sender, EventArgs e)
        {
            for (int i = m_FallPictureBox.Count - 1; i >= 0; i--)
            {
                PictureBox pictureBox = this.m_FallPictureBox[i];
                pictureBox.Top += 1;
                if (pictureBox.Bottom >= this.ClientSize.Height)
                {
                    m_FallPictureBox.RemoveAt(i);
                    this.Controls.Remove(pictureBox);
                    pictureBox.Dispose();
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                this.m_FallPictureBox.Add(pictureBox);
                this.m_PictureFallTimer.Start();
            }
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                clickedButton.Left += 10;
                clickedButton.Click -= randomButton_Click;
                clickedButton.Click += randomButtonSecond_Click;
            }
        }

        private void randomButtonSecond_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                clickedButton.Left -= 10;
            }
        }

        private void myForm_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form form)
            {
                form.Text = this.Size.ToString();
            }
        }

        private void myForm_LocationChanged(object sender, EventArgs e)
        {
            this.Text = this.Location.ToString();
        }

        /// <summary>
        /// Shows the form as a modal dialog box.
        /// </summary>
        public void showDialog_()
        {
            this.ShowDialog();
        }
    }
}