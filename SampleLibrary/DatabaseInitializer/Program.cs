using System;
using SampleLibrary;

namespace DatabaseInitializer
{
    internal static class Program
    {
        private static void Main2()
        {
            using var context = new SearchContext();
            context.Database.EnsureCreated();

            Searcher.SearchContext = context;
            var invertedIndex = new InvertedIndex();
            Searcher.InvertedIndex = invertedIndex;
            invertedIndex.IndexAllFiles("EnglishData");

            context.SaveChanges();

            Console.WriteLine("-------------------------");
            Console.WriteLine("All data has been added to Database");
        }
        private static void Main()//fot test
        {
            using var context = new SearchContext();
            Searcher.SearchContext = context;

            Searcher.InvertedIndex = new InvertedIndex();
            var result = Searcher.Search("hello");
            foreach (var s in result)
            {
                Console.WriteLine(s.FileName);
            }
        }

    }
}