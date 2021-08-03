using Xunit;

namespace SampleLibrary.Test
{
    public class InvertedIndexTest
    {

        [Fact]
        private void IndexAllFilesTest() {
            InvertedIndex invertedIndex = new InvertedIndex();
            // assertDoesNotThrow(() ->
            //     invertedIndex.indexAllFiles("src/main/resources/SampleEnglishData/EnglishData"));
            // assertNotNull(invertedIndex.getIndex());
        }

        [Fact]
        private void GetStopWordsTest() {
            InvertedIndex invertedIndex = new InvertedIndex();
            // assertEquals(InvertedIndex.getStopWords().size(), 119);
        }
    }
}