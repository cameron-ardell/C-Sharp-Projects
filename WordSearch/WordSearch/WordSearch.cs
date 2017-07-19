using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    public class WordSearch
    {
        public enum SolutionMethod
        {
            NAIVE, BINARY_SEARCH, USE_PREFIXES
        }

        // To hold the puzzle. 
        private static int MAX_ROWS = 50;
        private static int MAX_COLS = 50;
        char[,] board = new char[MAX_ROWS, MAX_COLS];

        // The actual number of rows and columns.
        int numRows;
        int numCols;

        // To signal that the puzzle is in a legal format
        bool goodPuzzle;

        // The user gets to specify the minimum word length
        int minWordLength;

        // Holds the words that we are searching for
        Dictionary dictionary;


        // The constructor creates the dictionary and reads the puzzle.
        // If the puzzle is good, it prints it out and gets the minimum 
        // length from the user
        public WordSearch()
        {

            dictionary = new Dictionary();

            goodPuzzle = readPuzzle();
            if (goodPuzzle)
            {
                printPuzzle();
                Console.WriteLine("Read puzzle with " + numRows + " rows and " + numCols + " columns");

                Console.WriteLine("Minimum word length: ");
                minWordLength = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }

        }

        public bool readPuzzle()
        {

            Console.WriteLine("Please enter the puzzle file name without the \".txt\" extension: ");
            String fileName = Console.ReadLine();
            String path = @"c:\Users\sarde\Desktop\C-Sharp-Projects\WordSearch\files\" + fileName + ".txt";

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);


            // Make sure the file has something in it.
            if ((line = file.ReadLine()) == null)
            {
                Console.WriteLine("Error: puzzle file is empty");
                numCols = 0;
                numRows = 0;
                return false;
            }

            // Get the first line; all the other lines should be
            // the same length (which is the number of columns).
            numCols = line.Length;
            // Put the first line in the board array.
            for (int i = 0; i < numCols; i++)
            {
                board[0,i] = Char.ToLower(line[i]);
            }

            // Read the rest of the puzzle lines and put them in 
            // the board array; keep track of the number of lines (rows)
            numRows = 1;
            while ((line = file.ReadLine()) != null)
            {
                // Make sure it’s the right length.
                if (line.Length != numCols)
                {
                    Console.WriteLine("Error: puzzle is not rectangular");
                    numRows = 0;
                    numCols = 0;
                    return false;
                }

                // Put this puzzle line in board.
                for (int i = 0; i < numCols; i++)
                {
                    board[numRows, i] = Char.ToLower(line[i]);
                }

                numRows++;
            }

            file.Close();
            
            // Puzzle was legal.
            return true;
        }

        // Purpose: This method returns the value of goodPuzzle,
        //          which indicates whether the current puzzle is
        //          in a legal format.
        //
        public bool isGoodPuzzle()
        {
            return goodPuzzle;
        }

        // Purpose: This method prints out the puzzle.
        // Parameters: None.
        // Return Value: None.
        //
        public void printPuzzle()
        {

            Console.WriteLine("\nThe Board: ");
            for (int r = 0; r < numRows; r++)
            {
                for (int c = 0; c < numCols; c++)
                {
                    if (board[r,c] == 0)
                        Console.Write("_ ");
				else {
                        String nextChar = board[r,c] + " ";
                        Console.Write(nextChar.ToUpper());
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        // Purpose: This method solves the puzzle with the specified method.
        // Parameters: The solution method to use.
        // Return Value: The time taken to solve the puzzle.
        //
        public double solve(SolutionMethod method)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();
   
            int numMatches = 0;

            if (method == SolutionMethod.NAIVE)
            {
                numMatches = solveNaively();
            }
            else if (method == SolutionMethod.BINARY_SEARCH)
            {
                numMatches = solveWithBinarySearch();
            }
            else if (method == SolutionMethod.USE_PREFIXES)
            {
                numMatches = solveWithPrefixes();
            }
;
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            double timeInSeconds = ts.TotalSeconds;


            Console.WriteLine("Found " + numMatches + " matches");

            return timeInSeconds;
        }




        // Directions: For every word in the dictionary, every starting point, and
        //             every direction, this method should call checkWord.
        public int solveNaively()
        {

            int numMatches = 0;

            for (int i = 0; i < dictionary.size(); i++)
            {
                String word = dictionary.get(i);

                if (word.Length >= minWordLength)
                {

                    // check all possible starting points
                    for (int row = 0; row < numRows; row++)
                    {
                        for (int col = 0; col < numCols; col++)
                        {

                            // don't bother checking in 8 directions unless the
                            // first character matches
                            if (board[row,col] == word[0])
                            {

                                // check all directions
                                for (int rowDir = -1; rowDir <= 1; rowDir++)
                                {
                                    for (int colDir = -1; colDir <= 1; colDir++)
                                    {
                                        if (rowDir != 0 || colDir != 0)
                                        {
                                            numMatches += checkWord(row, col, rowDir, colDir, word);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return numMatches;
        }



        // Directions: For a given starting point, direction, and word, check to
        //             see if that word exists starting at that point and going
        //             in that direction. Return 1, if found; otherwise, 0. This
        //             return value can be used in solveNaively to increment the
        //             count of words found.
        //
        // THE ABOVE IS NOT DOCUMENTATION! YOU SHOULD REPLACE IT WITH YOUR METHOD SUMMARY
        //
        public int checkWord(int baseRow, int baseCol,
                int rowDelta, int colDelta, String word)
        {

            int charCount = 1;
            int i, j;
            for (i = baseRow + rowDelta, j = baseCol + colDelta;
                    i >= 0 && j >= 0 && i < numRows && j < numCols && charCount < word.Length;
                    i += rowDelta, j += colDelta, charCount++)
            {

                // let's not be totally dumb - if we miss a char then bail
                if (board[i, j] != word[charCount])
                {
                    return 0;
                }
            }

            // if we exited early e.g. because we hit an edge
            if (charCount < word.Length)
            {
                return 0;
            }

            Console.WriteLine("Found \"" + word + "\" at (" + baseRow + "," +
                    baseCol + ") to (" + (i - rowDelta) + "," + (j - colDelta) + ")");

            return 1;

        }



        // Directions: For every starting point and every direction, call 
        //             checkDirectionUsingBinarySearch.
        //
        // THE ABOVE IS NOT DOCUMENTATION! YOU SHOULD REPLACE IT WITH YOUR METHOD SUMMARY
        //	
        public int solveWithBinarySearch()
        {
            int numMatches = 0;

            // all starting points
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {

                    // all directions
                    for (int rowDir = -1; rowDir <= 1; rowDir++)
                    {
                        for (int colDir = -1; colDir <= 1; colDir++)
                        {
                            if (rowDir != 0 || colDir != 0)
                            {
                                numMatches += checkDirectionUsingBinarySearch(row, col, rowDir, colDir);
                            }
                        }
                    }
                }
            }
            return numMatches;
        }



        // Directions: For the given starting point and direction, find all possible
        //             words that start there and go in that direction. Use binary
        //             search when checking for words in the dictionary.
        //
        //             NOTE: rowDelta and colDelta are the increments to baseRow
        //                   and baseCol that indicate the direction.
        //
        public int checkDirectionUsingBinarySearch(int baseRow, int baseCol,
                int rowDelta, int colDelta)
        {

            String charSequence;
            int numMatches = 0;
            bool inDictionary;

            charSequence = "" + board[baseRow,baseCol];

            for (int i = baseRow + rowDelta, j = baseCol + colDelta;
                    i >= 0 && j >= 0 && i < numRows && j < numCols;
                    i += rowDelta, j += colDelta)
            {
                charSequence += board[i, j];

                if (charSequence.Length >= minWordLength)
                {
                    inDictionary = dictionary.binarySearch(charSequence);
                }
                else
                {
                    inDictionary = false;
                }

                if (inDictionary)
                {
                    numMatches++;
                    Console.WriteLine("Found \"" + charSequence + "\" at (" + baseRow + ","
                            + baseCol + ") to (" + i + "," + j + ")");
                }
            }

            return numMatches;
        }



        // Directions: For every starting point and every direction, call 
        //             checkDirectionUsingPrefixes.
        public int solveWithPrefixes()
        {
            int numMatches = 0;

            // all starting points
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {

                    // all directions
                    for (int rowDir = -1; rowDir <= 1; rowDir++)
                    {
                        for (int colDir = -1; colDir <= 1; colDir++)
                        {

                            if (rowDir != 0 || colDir != 0)
                            {
                                numMatches += checkDirectionUsingPrefixes(row, col, rowDir, colDir);
                            }
                        }
                    }
                }
            }
            return numMatches;
        }





        // Directions: For the given starting point and direction, find all possible
        //             words that start there and go in that direction. Use the
        //             prefix version of binary search when checking for words in 
        //             the dictionary.
        //
        //             NOTE: rowDelta and colDelta are the increments to baseRow
        //                   and baseCol that indicate the direction.
        //
        public int checkDirectionUsingPrefixes(int baseRow, int baseCol,
                int rowDelta, int colDelta)
        {

            String charSequence;
            int numMatches = 0;
            bool inDictionary;

            charSequence = "" + board[baseRow, baseCol];

            for (int i = baseRow + rowDelta, j = baseCol + colDelta;
                    i >= 0 && j >= 0 && i < numRows && j < numCols;
                    i += rowDelta, j += colDelta)
            {
                charSequence += board[i,j];

                if (charSequence.Length >= minWordLength)
                {
                    inDictionary = dictionary.binarySearch(charSequence);
                }
                else
                {
                    inDictionary = false;
                }

                if (inDictionary && !dictionary.isPrefix(charSequence))
                {
                    break;
                }

                if (inDictionary)
                {
                    numMatches++;
                    Console.WriteLine("Found \"" + charSequence + "\" at (" + baseRow + ","
                            + baseCol + ") to (" + i + "," + j + ")");
                }
            }

            return numMatches;
        }
    }
}
