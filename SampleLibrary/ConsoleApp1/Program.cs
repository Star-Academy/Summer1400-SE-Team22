using System;
using SampleLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var _invertedIndex = new InvertedIndex();
            var _searcher = new Searcher();
            _invertedIndex.IndexAllFiles("EnglishData");
        }
    }
}