using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    public class Boggle
    {
        private static int NUM_ROWS = 4;
        private static int NUM_COLS = 4;
        private static int MIN_WORD_LENGTH = 3;

        private char[,] board = new char[NUM_ROWS, NUM_COLS];

        public enum Player
        {
            HUMAN, COMPUTER
        }

        private ArrayList humanWords;
        private ArrayList computerWords;
        private Dictionary dictionary;

        public Boggle()
        {
            dictionary = new Dictionary();
            readBoard();
            printPuzzle();
            humanWords = new ArrayList();
            computerWords = new ArrayList();


        }

        //prints board in upper case
        public void readBoard()
        {
            Console.WriteLine("Please enter the puzzle file name without the \".txt\" extension: ");
            String fileName = Console.ReadLine();
            String path = @"c:\Users\sarde\Desktop\C-Sharp-Projects\Boggle\Boggle\files\" + fileName + ".txt";

            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(path);


            // Make sure the file has something in it.
            if ((line = file.ReadLine()) == null)
            {
                Console.WriteLine("Error: puzzle file is empty");
                NUM_COLS = 0;
                NUM_ROWS = 0;
                return;
            }

            // Put the first line in the board array.
            for (int i = 0; i < NUM_COLS; i++)
            {
                board[0, i] = Char.ToLower(line[i]);
            }

            // Read the rest of the puzzle lines and put them in 
            // the board array; keep track of the number of lines (rows)
            int counter = 1;
            while ((line = file.ReadLine()) != null)
            {
                // Make sure it’s the right length.
                if (line.Length != NUM_COLS)
                {
                    Console.WriteLine("Error: puzzle is not rectangular");
                    NUM_ROWS = 0;
                    NUM_COLS = 0;
                    return;
                }

                // Put this puzzle line in board.
                for (int i = 0; i < NUM_COLS; i++)
                {
                    board[counter, i] = Char.ToLower(line[i]);
                }
                counter++;
            }

            // Make sure it’s the right length.
            if (counter != NUM_ROWS)
            {
                Console.WriteLine("Error: puzzle is not square");
                NUM_ROWS = 0;
                NUM_COLS = 0;
                return;
            }

            file.Close();

        }


        public void printPuzzle()
        {
            Console.WriteLine("\nThe Board:");
            for(int r = 0; r < NUM_ROWS; r++)
            {
                for(int c = 0; c < NUM_COLS; c++)
                {
                    if(board[r,c] == 0)
                    {
                        Console.Write("_ ");
                    } else
                    {
                        String nextChar = board[r, c] + " ";
                        Console.Write(nextChar.ToUpper());
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        // manages  play
        public void play()
        {

            // have the computer find all the words, but don't print and score;
            // just use it to check if human words are actually in the board
            computerPlay();

            Console.WriteLine("Would you like to make a feeble attempt to find some words before\n" +
                    "the computer finds them all and crushes your spirit? (y or n) ");
            String response = Console.ReadLine();

            if (response.ToLower().Equals("y"))
            {

                Console.WriteLine("\nEnter as many words as you want, one to a line.\n" +
                        "Enter an exclamation point when you are done.\n");

                String word = "";
                while (true)
                {

                    word = Console.ReadLine();
                    if (word.Equals("!"))
                        break;


                    if (!dictionary.binarySearch(word))
                    {
                        Console.WriteLine("    invalid word:  not in dictionary\n");
                    }

                    else if (word.Length < MIN_WORD_LENGTH)
                    {
                        Console.WriteLine("    invalid word:  words must have " + MIN_WORD_LENGTH +
                                " or more characters\n");
                    }

                    else if (!computerWords.Contains(word))
                    {
                        Console.WriteLine("    invalid word:  not in the board\n");
                    }

                    else if (alreadyUsed(word, humanWords))
                    {
                        Console.WriteLine("    invalid word:  already used\n");
                    }

                    else
                    {
                        Console.WriteLine("    Good word!\n");
                        humanWords.Add(word);
                    }

                }

                humanWords.Sort();
                printAndScoreWords(humanWords, Player.HUMAN);

            }

            // print and score computer words
            computerWords.Sort();
            printAndScoreWords(computerWords, Player.COMPUTER);

        }

        // for every position on the board, call generate words
        public void computerPlay()
        {

            for (int r = 0; r < NUM_ROWS; r++)
                for (int c = 0; c < NUM_COLS; c++)
                {

                    String word = "";
                    // now get every word at this position
                    generateWords(word, r, c);
                }

        }



        // generate all possible strings from [r,c] and check if they are in the dictionary 
        public void generateWords(String word, int r, int c)
        {

            // out of array bounds
            if (r < 0 || c < 0 || r >= NUM_ROWS || c >= NUM_COLS)
                return;

            // letter already used
            if (board[r,c] == ' ')
                return;

            // add the character, adjusting for q's
            String newWord = word + board[r,c];
            if (board[r,c] == 'q')
            {
                newWord += 'u';
            }
            // save this char and mark this spot as used by 
            // putting a space there
            char oldChar = board[r,c];
            board[r,c] = ' ';

            // check if the string is long enough, in the dictionary. and not already used
            if (newWord.Length >= MIN_WORD_LENGTH &&
                    dictionary.binarySearch(newWord) &&
                    !alreadyUsed(newWord, computerWords))
            {
                computerWords.Add(newWord);
            }

            // Try connected places
            generateWords(newWord, r, c - 1);
            generateWords(newWord, r, c + 1);
            generateWords(newWord, r - 1, c - 1);
            generateWords(newWord, r - 1, c);
            generateWords(newWord, r - 1, c + 1);
            generateWords(newWord, r + 1, c - 1);
            generateWords(newWord, r + 1, c);
            generateWords(newWord, r + 1, c + 1);

            // reset the square so we can use it again coming from another location
            board[r,c] = oldChar;
        }


        // checks to see if a particular word is already in the list
        public bool alreadyUsed(String word, ArrayList list)
        {

            return list.Contains(word);

        }




        public void printAndScoreWords(ArrayList list, Player whichPlayer)
        {

            int score = 0;

            if (whichPlayer == Player.HUMAN)
            {

                Console.WriteLine("\nHuman Words:");
                Console.WriteLine("============");
                for (int i = 0; i < humanWords.Count; i++)
                {
                    String word = (string)humanWords[i];
                    Console.WriteLine(word);
                    score += wordScore(word);
                }

                Console.WriteLine("\nTotal Score:  " + score + "\n\n");

            }

            if (whichPlayer == Player.COMPUTER)
            {

                Console.WriteLine("\nComputer Words:");
                Console.WriteLine("===============");

                for (int i = 0; i < computerWords.Count; i++)
                {
                    String word = (string)computerWords[i];
                    // don't let the computer use it if the human has it
                    if (alreadyUsed(word, humanWords))
                    {
                        Console.WriteLine(word + "    disallowed: found by human");
                    }
                    else
                    {
                        Console.WriteLine(word);
                        score += wordScore(word);
                    }
                }

                Console.WriteLine("\nTotal Score:  " + score + "\n\n");

            }

        }



        // of course, they don't need to use a switch statement
        public int wordScore(String word)
        {

            switch (word.Length)
            {

                case 0:
                case 1:
                case 2:
                    return 0;

                case 3:
                case 4:
                    return 1;

                case 5:
                    return 2;

                case 6:
                    return 3;

                case 7:
                    return 5;

                default:
                    return 11;

            }

        }

    }
}
