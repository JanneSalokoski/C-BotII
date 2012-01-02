using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerboGrease
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "MerboGrease BETA";
#if DEBUG
            Console.Title = "MerboGrease BETA (debug)";
#endif
            ProgramFunction.Run();
            Console.ReadKey();
            return;
        }
    }
}