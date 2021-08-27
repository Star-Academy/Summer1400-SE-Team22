using System;
using System.IO;

namespace SampleLibrary
{
    public static class FileReader
    {
        public static string ReadFileContent(string fileAddress)
        {
            try
            {
                return File.ReadAllText(fileAddress);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}