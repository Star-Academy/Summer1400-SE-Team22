using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Summer1400_SE_Team22
{
    internal class Program
    {
        private static void Main()
        {
            var studentsJson = File.ReadAllText("Database/Students.json");
            var students = JsonSerializer.Deserialize<Student[]>(studentsJson);

            var scoresJson = File.ReadAllText("Database/Scores.json");
            var scores = JsonSerializer.Deserialize<LessonScore[]>(scoresJson);

            foreach (var x in scores) students[x.StudentNumber - 1].AddAnScore(x.Score);
            foreach (var t in students) t.GPA = t.Scores.Average();

            foreach (var s in students.ToList().OrderByDescending(student => student.GPA).Take(3))
                Console.WriteLine($"-> name: {s.FirstName} {s.LastName}, GPA: {s.GPA:N2}");
        }
    }
}