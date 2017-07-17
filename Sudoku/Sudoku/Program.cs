using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            //get puzzle file name without extension
            Console.WriteLine("file name: ");
            string file = Console.ReadLine();

            // Read and print the puzzle
            DoSudoku puzzle = new DoSudoku(file);
            Console.WriteLine("The puzzle:");
            puzzle.printPuzzle();

            // Solve the puzzle and print solution
            puzzle.solve();
            Console.WriteLine("\nThe solution:");
            puzzle.printPuzzle();

            // Check the solution
            if(puzzle.checkSolution() == 0)
            {
                Console.WriteLine("\nSolution is correct!");
            }
            else
            {
                Console.WriteLine("\nSolution is NOT correct!");
            }


            Console.ReadKey();
        }
    }
}
