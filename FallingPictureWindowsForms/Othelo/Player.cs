using System;
using System.Drawing;

namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents a player in the Othello game.
    /// </summary>
    public class Player
    {
        private readonly string r_Name;
        private readonly Color r_Color;
        private int m_Score;
        private readonly char r_XOSymbol;
        public readonly bool r_IsComputer;

        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string Name
        {
            get { return this.r_Name; }
        }

        /// <summary>
        /// Gets the symbol (X or O) used by the player.
        /// </summary>
        public char XOSymbol
        {
            get { return this.r_XOSymbol; }
        }

        /// <summary>
        /// Gets or sets the score of the player.
        /// </summary>
        public int Score
        {
            get { return this.m_Score; }
            set { this.m_Score = value; }
        }

        /// <summary>
        /// Gets the color assigned to the player.
        /// </summary>
        public Color Color
        {
            get { return this.r_Color; }
        }

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="i_Name">The name of the player.</param>
        /// <param name="i_Color">The color assigned to the player.</param>
        /// <param name="i_IsComputer">Indicates whether this player is controlled by the computer.</param>
        public Player(string i_Name, Color i_Color, bool i_IsComputer)
        {
            this.r_Name = i_Name;
            this.r_Color = i_Color;
            this.m_Score = 0;
            this.r_IsComputer = i_IsComputer;
            this.r_XOSymbol = (i_Color == Color.Black) ? 'X' : 'O';
        }
    }
}