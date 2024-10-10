using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace FallingPictureWindowsForms
{
    internal class Cell
    {
        private string m_occupiedBy; // (Black, White, or None);
        private char m_inputCell; // ' ' , O , X
        public char m_InputCell
        {
            get { return this.m_inputCell; }
            set { this.m_inputCell = value; }
        }
        public string m_OccupiedBy
        {
            get { return this.m_occupiedBy; }
            set { this.m_occupiedBy = value; }
        }
        public Cell(string occupiedBy = null, char inputCell = ' ')
        {
            this.m_occupiedBy = occupiedBy;
            this.m_inputCell = inputCell;
        }
        public void UpdateCellStatus(string i_occupiedBy, char inputCell)
        {
            this.m_occupiedBy = i_occupiedBy;
            this.m_inputCell = inputCell;
        }
    }
}
