using System;
using System.IO;

namespace SampleLibrary
{
    public class FileReader
    {
        public static string ReadFileContent(string fileAddress)
        {
            return File.ReadAllText(fileAddress);
        }
    }
}