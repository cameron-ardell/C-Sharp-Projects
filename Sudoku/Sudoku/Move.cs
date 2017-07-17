using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /*
     * This class can be used to create objects that contain the specifications
     * for a Sudoku move: the digit, and the row and column of the space where
     * the digit is to be placed.
     */

    public class Move
    {
        // Used as a value for "digit" to indicate that no move was possible
        // in a given situation
        public static int NO_MOVE = 0;

        // Move information.
        private int row;
        private int col;
        private int digit;

        // Creates an "empty" Move
        public Move(int row, int col, int digit)
        {
            this.row = row;
            this.col = col;
            this.digit = digit;
        }

        public Move()
        {
            row = 0;
            col = 0;
            digit = 0;
        }

        // Standard getters and setters

        public int getRow()
        {
            return row;
        }

        public void setRow(int row)
        {
            this.row = row;
        }

        public int getCol()
        {
            return col;
        }

        public void setCol(int col)
        {
            this.col = col;
        }

        public int getDigit()
        {
            return digit;
        }

        public void setDigit(int digit)
        {
            this.digit = digit;
        }
    }
}
