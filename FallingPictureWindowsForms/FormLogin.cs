using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FallingPictureWindowsForms
{
    public class FormLogin : Form
    {
        private Label m_LabelUsername;
        private Label m_LabelPassword;
        private TextBox m_TextBoxUsername;
        private TextBox m_TextBoxPassword;
        private Button m_ButtonOk;
        private Button m_ButtonCancel;
        private bool m_closedByLogin = false;

        public string TextBoxUsername{
            get { return m_TextBoxUsername.Text; }
        }
        public string TextBoxPassword
        {
            get { return m_TextBoxPassword.Text; }
        }
        public bool ClosedByLogin
        {
            get { return m_closedByLogin; }
        }
        public FormLogin()
        {
            this.InitilizeLoginForm();
        }
        private void InitilizeLoginForm()
        {
            m_LabelUsername = new Label();
            m_LabelUsername.Text = "Username";
            m_LabelUsername.Left = 16;
            m_LabelUsername.Top = 16;
            this.Controls.Add(this.m_LabelUsername);

            m_LabelPassword = new Label();
            m_LabelPassword.Text = "Password";
            m_LabelPassword.Left = m_LabelUsername.Left;
            m_LabelPassword.Top = m_LabelUsername.Bottom + 20;
            this.Controls.Add(this.m_LabelPassword);

            m_TextBoxUsername = new TextBox();
            m_TextBoxUsername.Left = m_LabelUsername.Right + 20;
            m_TextBoxUsername.Top = m_LabelUsername.Top + m_LabelUsername.Height/2-m_TextBoxUsername.Height/2;
            this.Controls.Add(m_TextBoxUsername);

            m_TextBoxPassword= new TextBox();
            m_TextBoxPassword.Left = m_TextBoxUsername.Left;
            m_TextBoxPassword.Top = m_LabelPassword.Top + m_LabelPassword.Height/2-m_TextBoxPassword.Height/2;
            this.Controls.Add(m_TextBoxPassword);

            m_ButtonOk = new Button();
            m_ButtonOk.Text = "Login";
            m_ButtonOk.Left = m_TextBoxPassword.Right - m_ButtonOk.Width;
            m_ButtonOk.Top = m_TextBoxPassword.Bottom + 20;
            m_ButtonOk.Click += new EventHandler(CancelLoginClicked);
            this.Controls.Add(m_ButtonOk);

            m_ButtonCancel = new Button();
            m_ButtonCancel.Text = "Cancel";
            m_ButtonCancel.Left = m_ButtonOk.Left - 20 - m_ButtonCancel.Width;
            m_ButtonCancel.Top = m_ButtonOk.Top;
            m_ButtonCancel.Click += new EventHandler(CancelLoginClicked);
            this.Controls.Add(m_ButtonCancel);

            this.ClientSize = new Size(m_ButtonOk.Right + 20, m_ButtonOk.Bottom +20);
        }
        private void CancelLoginClicked(object sender, EventArgs e)
        {
            this.Close();
            if (sender == m_ButtonOk)
            {
                m_closedByLogin = true;
            }
        }
    }
}
