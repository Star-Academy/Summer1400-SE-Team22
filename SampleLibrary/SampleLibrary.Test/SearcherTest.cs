using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace SampleLibrary.Test
{
    public class SearcherTest : BeforeAfterTestAttribute
    {
        private static InvertedIndex _invertedIndex = new();
        private static Searcher _searcher = new();
        private TextWriter _savedOut;
        private TextReader _savedIn;


        public SearcherTest()
        {
            SetUpInvertedIndex("TestResources/EnglishData");

            SaveConsoleDefaults();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            RestoreDefaultConsole();
        }

        [Fact]
        private void NormalWordSearch()
        {
            var result = _searcher.Search("ali");
            Assert.Equal(7, result.Count);
            int i = 1;
            foreach (WordInfo wordInfo in
                result)
            {
                Assert.Equal("TestResources/EnglishData\\" + i, wordInfo.GetFileName());
                i++;
            }
        }

        [Fact]
        private void NormalWordsSearch()
        {
            var result = _searcher.Search("ali va hasan godratmand");
            Assert.Single(result);
            Assert.Equal("TestResources/EnglishData\\3", result[0].GetFileName());
        }

        [Fact]
        private void NormalWordsWithStopWordsBetweenSearch()
        {
            var result = _searcher.Search("ali va hasan is godratmand");
            Assert.Single(result);
            Assert.Equal("TestResources/EnglishData\\4", result[0].GetFileName());
        }


        [Fact]
        private void PlusWordsSearchOne()
        {
            var result = _searcher.Search("+ali va hasan godratmand");
            Assert.Equal(7, result.Count);
        }

        [Fact]
        private void PlusWordsSearchTwo()
        {
            var result = _searcher.Search("ali va +hasan godratmand");
            Assert.Equal(5, result.Count);
        }

        [Fact]
        private void MinusWordSearch()
        {
            var result = _searcher.Search("ali -hasan");
            Assert.Equal(2, result.Count);
        }

        [Fact]
        private void StopWordSearch()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var result = _searcher.Search("is");
            Assert.Null(result);
            Assert.Contains("please try a different keyword for your search!", output.ToString());
        }

        [Fact]
        private void StopWordsSearch()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var result = _searcher.Search("were is");
            Assert.Null(result);
            Assert.Contains("please try a different keyword for your search!", output.ToString());
        }

        [Fact]
        private void NotExistWordsSearch()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var result = _searcher.Search("inkalamehvojoodnadaradaziz");
            Searcher.PrintResults(result);
            Assert.Null(result);
            Assert.Contains("there is no match!", output.ToString());
        }

        [Fact]
        private void DeleteMinusWordsFromEmptyResultTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            Assert.Null(Record.Exception(() =>
                _searcher.Search("-kid")));
            Assert.Contains("please try a different keyword for your search!", output.ToString());
        }

        [Fact]
        private void MainTest()
        {
            SetUpInvertedIndex("EnglishData");

            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(() =>
                new Searcher().Run("EnglishData")));

            Assert.Contains("File name: ", output.ToString());
        }

        private static void SetUpInvertedIndex(string folder)
        {
            _invertedIndex = new InvertedIndex();
            _searcher = new Searcher();
            _invertedIndex.IndexAllFiles(folder);
            Searcher.InvertedIndex = _invertedIndex;
        }

        private void SaveConsoleDefaults()
        {
            _savedOut = Console.Out;
            _savedIn = Console.In;
        }

        private void RestoreDefaultConsole()
        {
            Console.SetOut(_savedOut);
            Console.SetIn(_savedIn);
        }

        [Fact]
        void MainTest2()
        {
            SetUpInvertedIndex("EnglishData");

            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello -kid\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(() =>
                new Searcher().Run("EnglishData")));
        }
    }
}