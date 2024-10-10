using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace FallingPictureWindowsForms
{
    class MyForm : Form
    {
        private Button m_ButtonStart;
        private Random m_Random = new Random();
        Timer m_PictureFallTimer = new Timer();
        private List<PictureBox> m_fallPictureBox=new List<PictureBox>();
        private Button m_ButtonLogin;
        private bool m_isLogin = false;
        private Button m_ButtonOthelloForm;
        public MyForm()
        {
            this.FormSetup();
        }
        public void FormSetup()
        {
            this.Text = "start game";
            this.Size = new Size(500, 500);
            this.Location = new Point(0, 0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.LocationChanged += MyForm_LocationChanged;
            //this.BackColor = Color.FromArgb(23, 104, 177);
            this.SizeChanged += MyForm_SizeChanged;
            m_ButtonStart = new Button();
            m_ButtonStart.Text = this.Text;
            //m_ButtonStart.BackColor = Color.Yellow;
            m_ButtonStart.Click += M_ButtonStart_Click;
            this.Controls.Add(m_ButtonStart);
            m_ButtonLogin = new Button();
            m_ButtonLogin.Text = "Login";
            //m_ButtonLogin.BackColor = Color.FromArgb(100,100,0);
            m_ButtonLogin.Location = new Point(0, 30);
            m_ButtonLogin.Click += M_ButtonLogin_Click;
            this.Controls.Add(m_ButtonLogin);
            //othelo game
            m_ButtonOthelloForm = new Button();
            m_ButtonOthelloForm.Text = "Othelo Game";
            m_ButtonOthelloForm.Location = new Point(30, 60);
            m_ButtonOthelloForm.Click += M_ButtonOthelloForm_Click;
            this.Controls.Add(m_ButtonOthelloForm);
        }

        private void M_ButtonOthelloForm_Click(object sender, EventArgs e)
        {
            OthelloForm formOthelo = new OthelloForm();
            formOthelo.StartPosition = FormStartPosition.CenterScreen;
            formOthelo.ShowDialog();
        }

        private void M_ButtonLogin_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.StartPosition = FormStartPosition.CenterScreen;
            formLogin.ShowDialog();
        }

        private void M_ButtonStart_Click(object sender, EventArgs e)
        {
            if (CheckLogedIn())
            {
                CreateRandomButtons();
            }
        }
        private bool CheckLogedIn()
        {
            if (!m_isLogin)
            {
                FormLogin formLogin = new FormLogin();
                formLogin.StartPosition = FormStartPosition.CenterScreen;
                formLogin.ShowDialog();
                if (formLogin.ClosedByLogin)
                {
                    if (Authentication.CheckUserCredentials(formLogin.TextBoxUsername, formLogin.TextBoxPassword))
                    {
                        m_isLogin = true;
                    }
                }
            }
            return m_isLogin;
        }
        private void CreateRandomButtons()
        {
            //button
            Button button = new Button();
            //button.BackColor = Color.Green;
            button.Width = 110;
            int location = m_Random.Next(this.ClientSize.Width -  button.Width);
            button.Left = location;
            button.Top = location;
            button.Click += Button_Click; 
            this.Controls.Add(button);
            //picturebox
            PictureBox pictureBox = new PictureBox();
            //pictureBox.BackColor = Color.White;
            pictureBox.Width = 60;
            pictureBox.Height = 60;
            pictureBox.Left= location - 20;
            pictureBox.Top = location - 20;
            pictureBox.Click += PictureBox_Click;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Load(@"C:\Users\itay\Videos\Captures\kali-moto.jpg");
            this.Controls.Add(pictureBox);
            //Timer for PictureBox
            this.m_PictureFallTimer.Interval = 100;
            this.m_PictureFallTimer.Tick += M_PictureFallTimer_Tick;
        }
        private void M_PictureFallTimer_Tick(object sender, EventArgs e)
        {
            PictureBox pictureBox;
            for(int i=0;i< m_fallPictureBox.Count(); i++)
            {
                pictureBox = this.m_fallPictureBox[i];
                pictureBox.Top += 1;
                if (pictureBox.Bottom >= this.ClientSize.Height)
                {
                    m_fallPictureBox.Remove(pictureBox);
                }
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            this.m_fallPictureBox.Add(sender as PictureBox);
            this.m_PictureFallTimer.Start();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            (sender as Button).Left += 10;  
            (sender as Button).Click -= Button_Click;
            (sender as Button).Click += Button_SecondClick;

        }
        private void Button_SecondClick(object sender, EventArgs e)
        {
            (sender as Button).Left -= 10;
        }
        private void MyForm_SizeChanged(object sender, EventArgs e)
        {
            (sender as Form).Text = this.Size.ToString();
        }
        private void MyForm_LocationChanged(object sender, EventArgs e)
        {
            this.Text = this.Location.ToString();
        }
        public void ShowDialog_()
        {
            this.ShowDialog();
        }
    }
}
