using System;
using SampleLibrary;

namespace DatabaseInitializer
{
    internal static class Program
    {
        private static void Main()
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
    }
}