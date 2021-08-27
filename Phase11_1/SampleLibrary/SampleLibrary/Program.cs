using System;

namespace SampleLibrary
{
    public static class Program
    {
        public static void Run(Searcher searcher)
        {
            while (true)
            {
                Console.WriteLine("enter a word for search:");
                var input = Console.ReadLine();
                if (input == "exit")
                {
                    return;
                }

                searcher.PrintResults(searcher.Search(input));
                Console.WriteLine("---------------------------------------------------");
            }
        }
    }
}