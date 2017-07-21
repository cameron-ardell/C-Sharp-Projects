using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Boggle!");
            Boggle boggleBoard = new Boggle();
            boggleBoard.play();
            Console.ReadKey();
        }
    }
}
