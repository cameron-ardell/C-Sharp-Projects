using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Word Search!");

            WordSearch puzzle = new WordSearch();

            if (puzzle.isGoodPuzzle())
            {
                Console.WriteLine("Using naive approach:");
                double solutionTime = puzzle.solve(WordSearch.SolutionMethod.NAIVE);
                Console.WriteLine("Solution time: " + solutionTime + " seconds.\n");

                Console.WriteLine("Using binary search approach:");
                solutionTime = puzzle.solve(WordSearch.SolutionMethod.BINARY_SEARCH);
                Console.WriteLine("Solution time: " + solutionTime + " seconds.\n");

                Console.WriteLine("Using binary search approach with prefixes:");
                solutionTime = puzzle.solve(WordSearch.SolutionMethod.USE_PREFIXES);
                Console.WriteLine("Solution time: " + solutionTime + " seconds.\n");
            }

            Console.ReadKey();

        }
    }
}
