using System.IO;
using Xunit;

namespace SampleLibrary.Test
{
    public class FileReaderTest
    {
        [Fact]
        private void ReadFileContentTest()
        {
            File.WriteAllTextAsync("temp.txt", "hello world!");

            var content = FileReader.ReadFileContent("temp.txt");

            Assert.Equal("hello world!", content);

            File.Delete("temp.txt");
        }

        [Fact]
        private void ReadNotExistingFileContentTest()
        {
            Assert.NotNull(Record.Exception(() => FileReader.ReadFileContent("src/main/resources/notExistingFile.xyz")));
        }
    }
}