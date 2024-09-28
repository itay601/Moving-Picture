﻿using System;
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
            this.BackColor = Color.FromArgb(23, 104, 177);
            this.SizeChanged += MyForm_SizeChanged;
            m_ButtonStart = new Button();
            m_ButtonStart.Text = this.Text;
            m_ButtonStart.BackColor = Color.Yellow;
            m_ButtonStart.Click += M_ButtonStart_Click;
            this.Controls.Add(m_ButtonStart);   
        }

        private void M_ButtonStart_Click(object sender, EventArgs e)
        {
            CreateRandomButtons();
        }

        private void CreateRandomButtons()
        {
            //button
            Button button = new Button();
            button.BackColor = Color.Green;
            button.Width = 110;
            int location = m_Random.Next(this.ClientSize.Width -  button.Width);
            button.Left = location;
            button.Top = location;
            button.Click += Button_Click; 
            this.Controls.Add(button);
            //picturebox
            PictureBox pictureBox = new PictureBox();
            pictureBox.BackColor = Color.White;
            pictureBox.Width = 60;
            pictureBox.Height = 60;
            pictureBox.Left= location - 20;
            pictureBox.Top = location - 20;
            pictureBox.Click += PictureBox_Click;
            //pictureBox.Click += new ElventHandler(PictureBox_Click);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Load(@"C:\Users\itay\Videos\Captures\kali-moto.jpg");
            this.Controls.Add(pictureBox);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            (sender as PictureBox).Top -= 1;
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
            //throw new NotImplementedException();

        }

        public void ShowDialog_()
        {
            this.ShowDialog();
        }


    }
}
