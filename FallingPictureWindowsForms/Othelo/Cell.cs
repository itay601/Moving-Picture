namespace FallingPictureWindowsForms
{
    /// <summary>
    /// Represents a cell on the Othello board.
    /// </summary>
    internal class Cell
    {
        private string m_OccupiedBy; // (Black, White, or None)
        private char m_InputCell; // ' ' , 'O' , 'X'

        /// <summary>
        /// Gets or sets the character representation of the cell's content.
        /// </summary>
        public char InputCell
        {
            get { return this.m_InputCell; }
            set { this.m_InputCell = value; }
        }

        /// <summary>
        /// Gets or sets the string representation of who occupies the cell.
        /// </summary>
        public string OccupiedBy
        {
            get { return this.m_OccupiedBy; }
            set { this.m_OccupiedBy = value; }
        }

        /// <summary>
        /// Initializes a new instance of the Cell class.
        /// </summary>
        /// <param name="i_OccupiedBy">Who occupies the cell (default is null).</param>
        /// <param name="i_InputCell">The character representation of the cell (default is space).</param>
        public Cell(string i_OccupiedBy = null, char i_InputCell = ' ')
        {
            this.m_OccupiedBy = i_OccupiedBy;
            this.m_InputCell = i_InputCell;
        }

        /// <summary>
        /// Updates the status of the cell.
        /// </summary>
        /// <param name="i_OccupiedBy">Who now occupies the cell.</param>
        /// <param name="i_InputCell">The new character representation of the cell.</param>
        public void updateCellStatus(string i_OccupiedBy, char i_InputCell)
        {
            this.m_OccupiedBy = i_OccupiedBy;
            this.m_InputCell = i_InputCell;
        }
    }
}