using System;
using SampleLibrary;

namespace DatabaseInitializer
{
    internal static class Program
    {
        private static void Main()
        {
            InitializeDatabase();
        }

        private static void InitializeDatabase()
        {
            using var context = new SearchContext();
            context.Database.EnsureCreated();

            var searcher = new Searcher(context, new InvertedIndex(context));
            Searcher.InvertedIndex.IndexAllFiles("EnglishData");

            context.SaveChanges();

            Console.WriteLine("-------------------------");
            Console.WriteLine("All data has been added to Database");
        }
    }
}