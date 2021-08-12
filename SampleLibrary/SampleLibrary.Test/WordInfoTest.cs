using Xunit;

namespace SampleLibrary.Test
{
    public class WordInfoTest
    {
        [Fact]
        private void GetFileNameTest()
        {
            var wordInfo = new WordInfo("test", 20);
            Assert.Equal("test", wordInfo.GetFileName());
        }

        [Fact]
        private void GetPositionTest()
        {
            var wordInfo = new WordInfo("test2", 30);
            Assert.Equal(30, wordInfo.GetPosition());
        }
    }
}