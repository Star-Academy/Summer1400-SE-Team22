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
        private static InvertedIndex invertedIndex = new();
        private static Searcher searcher = new();


        public override void Before(MethodInfo methodUnderTest)
        {
            invertedIndex = new InvertedIndex();
            searcher = new Searcher();
            invertedIndex.IndexAllFiles("src/test/java/TestResources/EnglishData");
            Searcher.InvertedIndex = (invertedIndex);
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
            Assert.Equal(1, result.Count);
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
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            // List<WordInfo> result = searcher.Search("inkalamehvojoodnadaradaziz");
            // Method printResultMethod = Class.forName("Searcher").getDeclaredMethod("printResults", List.class);
            // printResultMethod.setAccessible(true);
            // printResultMethod.invoke(searcher, result);
            // Assert.Equal(null, result);
            // assert(outContent.toString().contains("there is no match!"));
        }

        [Fact]
        private void DeleteMinusWordsFromEmptyResultTest()
        {
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            // Assertions.assertDoesNotThrow(()->searcher.Search("-kid"));
            // assert(outContent.toString().contains("please try a different keyword for your search!"));
        }

        [Fact]
        private void MainTest()
        {
            var savedOut = Console.Out;
            var savedIn = Console.In;

            var output = new StringWriter();
            Console.SetOut(output);

            var data = String.Join(Environment.NewLine, "hello\nexit");
            Console.SetIn(new StringReader(data));

            Assert.Null(Record.Exception(() =>
                new Searcher().Run("EnglishData")));

            Assert.Contains("File name: ", output.ToString());

            Console.SetOut(savedOut);
            Console.SetIn(savedIn);
        }

        [Fact]
        void MainTest2()
        {
            // InputStream sysInBackup = System.in;
            // ByteArrayInputStream in = new ByteArrayInputStream(("hello -kid\nexit").getBytes());
            // System.setIn(in);
            //
            // ByteArrayOutputStream outContent = new ByteArrayOutputStream();
            // System.setOut(new PrintStream(outContent));
            // outContent.reset();
            //
            // assertDoesNotThrow(()->Main.main(null));
        }
    }
}