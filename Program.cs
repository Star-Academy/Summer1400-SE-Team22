using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Summer1400_SE_Team22
{
    class Program
    {
        static void Main()
        {
            var studentsJson = File.ReadAllText("Database/Students.json");
            var students = JsonSerializer.Deserialize<Student[]>(studentsJson);

            var scoresJson = File.ReadAllText("Database/Scores.json");
            var scores = JsonSerializer.Deserialize<LessonScore[]>(scoresJson);

            foreach (var x in scores) students[x.StudentNumber - 1].AddAnScore(x.Score);

            CalculateGPA(students);

            PrintResult(students.ToList().OrderByDescending(student => student.GPA).ToList());

        }

        private static void PrintResult(List<Student> studentsList)
        {
            for (int i = 0; i < 3; i++)
                Console.WriteLine("Rank: " + (i + 1) + ", name: " + studentsList.ElementAt(i).FirstName + " " +
                                  studentsList.ElementAt(i).LastName + ", GPA: " +
                                  studentsList.ElementAt(i).GPA.ToString("N2"));
        }

        private static void CalculateGPA(IEnumerable<Student> students)
        {
            foreach (var t in students)
            {
                t.GPA = t.Scores.Average();
            }
        }
    }

    public class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<double> Scores = new List<double>();
        public double GPA { get; set; } = 0;

        public void AddAnScore(double score)
        {
            Scores.Add(score);
        }
    }

    public class LessonScore
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public double Score { get; set; }
    }
}