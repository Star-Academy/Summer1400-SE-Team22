using System.IO;

namespace SampleLibrary
{
    public static class FileReader
    {
        public static string ReadFileContent(string fileAddress)
        {
            return File.ReadAllText(fileAddress);
        }

        public static string GetConnectionString()
        {
            return ReadFileContent("connectionString.json");
        }
    }
}