using System.IO;
using Xunit;

namespace SampleLibrary.Test
{
    public class FileReaderTest
    {
        [Fact]
        private void ReadFileContentTest()
        {
            const string tempFileName = "temp.txt";
            const string tempFileContent = "hello world!";

            File.WriteAllTextAsync(tempFileName, tempFileContent);

            var content = FileReader.ReadFileContent(tempFileName);

            Assert.Equal(tempFileContent, content);

            File.Delete(tempFileName);
        }

        [Fact]
        private void ReadNotExistingFileContentTest()
        {
            const string notExistingFilePath = "src/main/resources/notExistingFile.xyz";
            Assert.Equal("", FileReader.ReadFileContent(notExistingFilePath));
        }
    }
}