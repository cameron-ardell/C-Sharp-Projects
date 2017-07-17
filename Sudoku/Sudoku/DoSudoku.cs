using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class DoSudoku
    {
        private static int BLANK = 0;

        // A standard Sudoku puzzle is 9x9.
        private static int DIMENSION = 9;

        // Arrays to hold the puzzle and the actual solution so that
        // we can check the program-generated solution against the
        // actual solution.
        private int[,] puzzle = new int[DIMENSION, DIMENSION];
        private int[,] solution = new int[DIMENSION, DIMENSION];

        // This constructor takes the name of a puzzle file and calls
        // readPuzzle to read the puzzle into the puzzle array.
        public DoSudoku(String fileName)
        {
            readPuzzle(fileName);
        }

        // Purpose: Read the puzzle into the puzzle array.
        // Parameters: 
        // Return Value:
        //
        //              NOTE: The program assumes that blank
        //                    spaces are indicated by zeros in
        //                    the puzzlefile.
        public void readPuzzle(String fileName)
        {
            // reading in the puzzle
            string path = @"c:\Users\sarde\Desktop\C-Sharp-Projects\Sudoku\Sudoku\" + fileName + ".txt";
            string line;
            int counter = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                int[] newLine = toIntArray(line, ' ');
                for(int i = 0; i < DIMENSION; i++)
                {
                    puzzle[counter, i] = newLine[i];
                }
                counter++;
            }
            file.Close();

            // reading in the solution
            // reading in the puzzle
            path = @"c:\Users\sarde\Desktop\C-Sharp-Projects\Sudoku\Sudoku\"
+ fileName + "-solution.txt";
            counter = 0;
            System.IO.StreamReader file2 = new System.IO.StreamReader(path);
            while ((line = file2.ReadLine()) != null)
            {
                int[] newLine = toIntArray(line, ' ');
                for (int i = 0; i < DIMENSION; i++)
                {
                    solution[counter, i] = newLine[i];
                }
                counter++;
            }
            file2.Close();

            //Console.WriteLine("PUZZLE: ");
            //for(int i = 0; i < DIMENSION; i++)
            //{
            //    for(int j = 0; j < DIMENSION; j++)
            //    {
            //        Console.Write(puzzle[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine("\n\nSOLUTION: ");
            //for (int i = 0; i < DIMENSION; i++)
            //{
            //    for (int j = 0; j < DIMENSION; j++)
            //    {
            //        Console.Write(solution[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
        }

        public static int[] toIntArray(string value, char separator)
        {
            return Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        }


        // Purpose: Solve the puzzle.
        // Parameters: None.
        // Return Value: None.
        //
        public void solve()
        {

            Stack<Move> stack = new Stack<Move>();

            while (!success())
            {

                Move nextMove = NextMove();

                // If thisMove.getDigit() != Move.NO_MOVE, it means that a legal move
                // was found, so push onto the stack.
                if (nextMove.getDigit() != Move.NO_MOVE)
                {
                    makeMove(nextMove.getRow(), nextMove.getCol(), nextMove.getDigit());
                    stack.Push(nextMove);
                }

                // If a legal move not found, we need to backtrack, possibly more
                // than one move.
                else
                {
                    bool backtrack = true;

                    while (backtrack)
                    {
                        // Get the move that got us into trouble.
                        Move lastMove = stack.Pop();
                        // Reset the square on the board to indicate that it is blank.
                        makeMove(lastMove.getRow(), lastMove.getCol(), Move.NO_MOVE);
                        // Try to change the move at that square, i.e.
                        // send the last move tried to changeMove, so changeMove knows
                        // what has already been tried.
                        Move tryMove = ChangeMove(lastMove.getDigit(), lastMove.getRow(), lastMove.getCol());
                        // If the move returned by changeMove was legal, push it on the stack
                        // and reset backtrack; otherwise, backtrack will remain true, and we'll
                        // pop another move off the stack.
                        if (tryMove.getDigit() != Move.NO_MOVE)
                        {
                            makeMove(tryMove.getRow(), tryMove.getCol(), tryMove.getDigit());
                            stack.Push(tryMove);
                            backtrack = false;
                        }

                    }  // end while (backtrack)

                }  // end else

            } // end while (!success())

        }



        // Purpose: Get the digit from a specified location in the puzzle.
        // Parameters: The row and column we want to get the digit of.
        // Return Value: The digit at that row and column.
        //
        public int getDigit(int row, int col)
        {
            return puzzle[row, col];
        }


        // 
        // Purpose: Put a digit in a specified location in the puzzle.
        // Parameters: The digit to be palced and the row and column 
        //             that it should be placed at.
        // Return Value: None.
        //
        public void makeMove(int row, int col, int digit)
        {
            puzzle[row, col] = digit;
        }


        // Purpose: Change the move at a specified location, if possible.
        // Parameters: The row and column of the specified location and
        //             the most recent digit tried there,
        // Return Value: A Move object that contains the move found or
        //               a value indicating that there are no more legal
        //               moves at that location.
        //
        public Move ChangeMove(int lastDigit, int row, int col)
        {

            // The Move object to return.
            Move move = new Move();

            // Try all the digits that come after lastDigit.
            for (int next = lastDigit + 1; next <= DIMENSION; next++)
            {

                // If the move is legal, return it.
                if (legalMove(row, col, next))
                {
                    move.setRow(row);
                    move.setCol(col);
                    move.setDigit(next);
                    return move;
                }
            }

            // No legal moves were found, so set the digit to indicate
            // that and return the Move.
            move.setDigit(Move.NO_MOVE);
            return move;
        }


        // Purpose: Find the first empty space in the puzzle (going row by
        //          row, left to right) and try to find a legal move for that
        //          space.
        // Parameters: None.
        // Return Value: A Move object that contains the move found for the
        //               first empty space or a value indicating that there 
        //               were no legal moves at that location.
        //
        public Move NextMove()
        {

            // The Move object to return.
            Move move = new Move();


            // Go through row byrow, left to right.
            for (int row = 0; row < DIMENSION; row++)
            {
                for (int col = 0; col < DIMENSION; col++)
                {

                    if (puzzle[row, col] == BLANK)
                    {

                        // Try all possible digits.
                        for (int next = 1; next <= DIMENSION; next++)
                        {

                            // If the move is legal, return it.
                            if (legalMove(row, col, next))
                            {
                                move.setRow(row);
                                move.setCol(col);
                                move.setDigit(next);
                                return move;
                            }
                        }

                        // No legal moves were found, so set the digit 
                        // to indicate that and return the Move.
                        move.setDigit(Move.NO_MOVE);
                        return move;
                    }

                }
            }

            // We should never get here, because if there are no more
            // empty places, the puzzle is solved, but we have to
            // return something.
            return null;
        }


        // Purpose: Check if a move is legal, given the state of the puzzle.
        // Parameters: The row and column of the move and the digit to 
        //             be palced there.
        // Return Value: True, if the move is legal; otherwise, false.
        //
        public bool legalMove(int row, int col, int digit)
        {

            // Check the row and column.
            for (int i = 0; i < DIMENSION; i++)
            {
                if (puzzle[row, i] == digit || puzzle[i, col] == digit)
                {
                    return false;
                }
            }

            // Check the 3x3 group that [row,col] belongs to. Note that
            // rGroup and cGroup are essentially indices of 3x3 groups.
            // In other words, they are indexed as if they were individual
            // entities, e.g. if rGroup and cGroup are both 1, they refer
            // to the 3x3 group in the center of the puzzle.
            int rGroup = row / 3;
            int cGroup = col / 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (puzzle[rGroup * 3 + i, cGroup * 3 + j] == digit)
                    {
                        return false;
                    }
                }
            }

            // If we never found a problem and returned false, it's legal.
            return true;
        }


        // Purpose: Check to see if the puzzle has been successfully completed.
        // Parameters: None.
        // Return Value: True, if the puzzle has been successfully completed; 
        //               otherwise, false.
        //               NOTE: 	Since we have ensured that we make only legal
        //                      moves, we have solved the puzzle if there are
        //                      no more blank spaces.
        // 
        public bool success()
        {
            for (int r = 0; r < DIMENSION; r++)
            {
                for (int c = 0; c < DIMENSION; c++)
                {
                    if (puzzle[r,c] == 0)
                        return false;
                }
            }
            return true;
        }


        // Purpose: Print out the puzzle in a nicely formatted way.
        // Parameters: None.
        // Return Value: None.
        //
        public void printPuzzle()
        {

            for (int r = 0; r < DIMENSION; r++)
            {
                for (int c = 0; c < DIMENSION; c++)
                {
                    if (puzzle[r,c] == 0)
                        Console.Write("_ ");
				else
                        Console.Write(puzzle[r,c] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }


        // Purpose: Check the computed solution against the actual solution.
        // Parameters: None.
        // Return Value: The number of errors found.
        //
        public int checkSolution()
        {

            int numErrors = 0;

            for (int r = 0; r < DIMENSION; r++)
            {
                for (int c = 0; c < DIMENSION; c++)
                {
                    if (puzzle[r,c] != solution[r,c])
                        ++numErrors;
                }
            }

            return numErrors;
        }
    }







}
