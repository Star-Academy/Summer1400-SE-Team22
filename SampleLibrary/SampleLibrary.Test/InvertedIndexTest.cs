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
                invertedIndex.IndexAllFiles("EnglishData"));

            Assert.Null(exception);
            Assert.NotNull(invertedIndex.Index);
        }

        [Fact]
        private void GetStopWordsTest()
        {
            Assert.Equal(119, InvertedIndex.StopWords.Count);
        }
    }
}