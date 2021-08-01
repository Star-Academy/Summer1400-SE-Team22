using System;
using System.IO;

namespace Summer1400_SE_Team22
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader("Database/Students.json"))
            {
                var serializedItem = JsonSerializer.De(item);
            }

        }
    }
    
}
