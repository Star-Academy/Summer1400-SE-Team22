using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Summer1400_SE_Team22
{
    internal static class Program
    {
        private static void Main()
        {
            Run();
        }

        private static void Run()
        {
            const string studentsFilePath = "Database/Students.json";
            const string scoresFilePath = "Database/Scores.json";

            var students = JsonDeserializer.Deserialize<Student[]>(studentsFilePath);
            var scores = JsonDeserializer.Deserialize<LessonScore[]>(scoresFilePath);

            GPACalculator.CalculateGPA(scores, students);

            ResultPrinter.PrintResults(students, 3);
        }
    }
}