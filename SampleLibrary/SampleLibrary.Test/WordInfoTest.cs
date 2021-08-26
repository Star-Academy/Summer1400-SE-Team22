using Xunit;

namespace SampleLibrary.Test
{
    public class WordInfoTest
    {
        [Fact]
        private void GetFileNameTest()
        {
            const string fileName = "test";
            var wordInfo = new WordInfo(fileName, 20);
            Assert.Equal(fileName, wordInfo.GetFileName());
        }

        [Fact]
        private void GetPositionTest()
        {
            const string fileName = "test2";
            var wordInfo = new WordInfo(fileName, 30);
            Assert.Equal(30, wordInfo.GetPosition());
        }
    }
}