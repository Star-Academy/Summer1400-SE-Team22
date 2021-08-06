﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace SampleLibrary.Test
{
    public class SearcherTest : BeforeAfterTestAttribute
    {
        private static InvertedIndex invertedIndex = new();
        private static Searcher searcher = new();
        private TextWriter _savedOut;
        private TextReader _savedIn;


        public override void Before(MethodInfo methodUnderTest)
        {
            invertedIndex = new InvertedIndex();
            searcher = new Searcher();
            invertedIndex.IndexAllFiles("src/test/java/TestResources/EnglishData");
            Searcher.InvertedIndex = invertedIndex;

            SaveConsoleDefaults();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            RestoreDefaultConsole();
        }

        [Fact]
        private void NormalWordSearch()
        {
            List<WordInfo> result = searcher.Search("ali");
            Assert.Equal(7, result.Count);
            int i = 1;
            foreach (WordInfo wordInfo in
                result)
            {
                Assert.Equal(i + "", wordInfo.GetFileName());
                i++;
            }
        }

        [Fact]
        private void NormalWordsSearch()
        {
            List<WordInfo> result = searcher.Search("ali va hasan godratmand");
            Assert.Single(result);
            Assert.Equal("3", result[0].GetFileName());
        }

        [Fact]
        private void NormalWordsWithStopWordsBetweenSearch()
        {
            var result = searcher.Search("ali va hasan is godratmand");
            Assert.Single(result);
            Assert.Equal("4", result[0].GetFileName());
        }


        [Fact]
        private void PlusWordsSearchOne()
        {
            List<WordInfo> result = searcher.Search("+ali va hasan godratmand");
            Assert.Equal(7, result.Count);
        }

        [Fact]
        private void PlusWordsSearchTwo()
        {
            List<WordInfo> result = searcher.Search("ali va +hasan godratmand");
            Assert.Equal(5, result.Count);
        }

        [Fact]
        private void MinusWordSearch()
        {
            List<WordInfo> result = searcher.Search("ali -hasan");
            Assert.Equal(2, result.Count);
        }

        [Fact]
        private void StopWordSearch()
        {
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            // List<WordInfo> result = searcher.Search("is");
            // Assert.Equal(null, result);
            // assert(outContent.toString().contains("please try a different keyword for your search!"));
        }

        [Fact]
        private void StopWordsSearch()
        {
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            // List<WordInfo> result = searcher.Search("were is");
            // Assert.Equal(null, result);
            // assert(outContent.toString().contains("please try a different keyword for your search!"));
        }

        [Fact]
        private void NotExistWordsSearch()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            List<WordInfo> result = searcher.Search("inkalamehvojoodnadaradaziz");
            new Searcher().printResults(result);
            Assert.Null(result);
            Assert.Contains("there is no match!", output.ToString());
        }

        [Fact]
        private void DeleteMinusWordsFromEmptyResultTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            Assert.Null(Record.Exception(() =>
                searcher.Search("-kid")));
            Assert.Contains("please try a different keyword for your search!", output.ToString());
        }

        [Fact]
        private void MainTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(() =>
                new Searcher().Run("EnglishData")));

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
        void MainTest2()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var data = string.Join(Environment.NewLine, "hello -kid\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(() =>
                new Searcher().Run("EnglishData")));
        }
    }
}