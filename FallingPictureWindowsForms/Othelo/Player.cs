using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace FallingPictureWindowsForms
{
    enum colorAA
    {
        Red,
        Blue,
        None,
        // blue/red or white
    }
    public class Player
    {
        private readonly string m_name;
        private Color m_color;
        private int m_score;
        private char m_OXSymbole;
        public bool m_isComputer;
        public string Name
        {
            get { return this.m_name; }
        }
        public bool IsComputer
        {
            get { return this.IsComputer; }
        }
        public char XOSymbole
        {
            get { return this.m_OXSymbole; }
        }
        public int Score
        {
            get { return this.m_score; }
            set { this.m_score = value; }
        }
        public Color color
        {
            get { return this.m_color; }
            set { this.m_color = value; }
        }
        public Player(string name, Color color, bool isComputer)
        {
            this.m_name = name;
            this.m_color = color;
            this.m_score = 0;
            this.m_isComputer = isComputer;
            if (m_color == Color.Red)
            {
                m_OXSymbole = 'X';
            }
            else
            {
                m_OXSymbole = 'O';
            }
        }
    }
}