using Xunit;

namespace SampleLibrary.Test
{
    public class InvertedIndexTest
    {
        [Fact]
        private void IndexAllFilesTest()
        {
            const string folderAddress = "EnglishData";
            var invertedIndex = new InvertedIndex();

            var exception = Record.Exception(() =>
                invertedIndex.IndexAllFiles(folderAddress));

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