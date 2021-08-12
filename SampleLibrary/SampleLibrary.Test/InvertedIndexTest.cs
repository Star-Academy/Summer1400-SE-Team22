using System.IO;
using Xunit;

namespace SampleLibrary.Test
{
    public class InvertedIndexTest
    {
        [Fact]
        private void IndexAllFilesTest()
        {
            var invertedIndex = new InvertedIndex();

            var exception = Record.Exception(() =>
                invertedIndex.IndexFile(Directory.GetFiles("EnglishData")[0]));

            Assert.Null(exception);
            Assert.NotNull(invertedIndex.Words);
        }

        [Fact]
        private void GetStopWordsTest()
        {
            Assert.Equal(119, new InvertedIndex().StopWords.Count);
        }
    }
}