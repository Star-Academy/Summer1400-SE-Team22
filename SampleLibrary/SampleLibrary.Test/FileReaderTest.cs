using System;
using System.IO;
using Xunit;

namespace SampleLibrary.Test
{
    public class FileReaderTest
    {
        [Fact]
        private void ReadFileContentTest()
        {
            // try {
            //     FileWriter fileWriter = new FileWriter("src/main/resources/temp.txt");
            //     fileWriter.write("hello world!");
            //     fileWriter.close();
            // } catch (IOException e) {
            //     e.printStackTrace();
            // }
            //
            // File tempFile = new File("src/main/resources/temp.txt");
            // String content = FileReader.readFileContent(tempFile);
            //
            // assertEquals(content, "hello world!");
            //
            // tempFile.delete();
        }

        [Fact]
        private void ReadNotExistingFileContentTest()
        {
            // assertDoesNotThrow(() ->{
            //     FileReader.readFileContent(new File("src/main/resources/notExistingFile.xyz"));
            // });
        }
    }
}