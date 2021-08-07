﻿using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Summer1400_SE_Team22
{
    class Program
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

    public abstract class Student
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

    public abstract class LessonScore
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public double Score { get; set; }
    }
}