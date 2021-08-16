using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace SampleLibrary.Test
{
    public class SearcherTest : BeforeAfterTestAttribute
    {
        private TextReader _savedIn;
        private TextWriter _savedOut;

        private readonly Searcher _searcher;


        public SearcherTest()
        {
            var searchContext = new SearchContext();
            _searcher = new Searcher(searchContext, new InvertedIndex(searchContext));
            SaveConsoleDefaults();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            RestoreDefaultConsole();
        }

        [Fact]
        private void NormalWordSearch()
        {
            var result = _searcher.Search("hello");
            Assert.Equal(8, result.Count);
        }

        [Fact]
        private void NormalWordsSearch()
        {
            var result = _searcher.Search("school");
            Assert.True(result.Count > 10);
        }

        [Fact]
        private void NormalWordsWithStopWordsBetweenSearch()
        {
            var result = _searcher.Search("boy");
            Assert.True(result.Count == 12);
        }


        [Fact]
        private void PlusWordsSearchOne()
        {
            var result = _searcher.Search("short");
            Assert.True(result.Count > 20);
        }

        [Fact]
        private void PlusWordsSearchTwo()
        {
            var result = _searcher.Search("-jump");
            Assert.Null(result);
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
            _searcher.PrintResults(result);
            Assert.True(result.Count == 0);
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
            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(_searcher.Run));

            Assert.Contains("File name: ", output.ToString());
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
        private void MainTest2()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello -kid\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(_searcher.Run));
        }
    }
}