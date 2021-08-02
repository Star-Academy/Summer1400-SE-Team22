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
        static void Main(string[] args)
        {
            string studentsJson = File.ReadAllText("Database/Students.json");
            Student[] students = JsonSerializer.Deserialize<Student[]>(studentsJson);

            string scoresJson = File.ReadAllText("Database/Scores.json");
            LessonScore[] scores = JsonSerializer.Deserialize<LessonScore[]>(scoresJson);
            
            for (int i = 0; i < scores.Length; i++)
            {
                students[scores[i].StudentNumber - 1].AddAnScore(scores[i].Score);
            }
            
            CalculateGPA(students);

            List<Student> studentsList = students.ToList().OrderByDescending(student => student.GPA).ToList();

            PrintResult(studentsList);
        }

        private static void PrintResult(List<Student> studentsList){
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Rank: " + (i+1) + ", name: " + studentsList.ElementAt(i).FirstName + " " + studentsList.ElementAt(i).LastName + ", GPA: " + studentsList.ElementAt(i).GPA.ToString("N2")) ;
            }
        }

        private static void CalculateGPA(Student[] students){
            for (int i = 0; i < students.Length; i++)
            {
                double sumOfGrades = 0;
                for (int j = 0 ; j < students[i].Scores.Count; j++) {
                    sumOfGrades += students[i].Scores.ElementAt(j);
                }
                students[i].GPA = (double)(sumOfGrades / students[i].Scores.Count);
            }          
        }
    }

    public class Student
    {
         public int StudentNumber {get; set;}
         public string FirstName {get; set;}
         public string LastName {get; set;} 
         public List<double> Scores = new List<double>();
         public double GPA {get; set;} = 0 ;

         public void AddAnScore(double score){
             Scores.Add(score);
         }
    }

    public class LessonScore
    {
      public int StudentNumber {get; set;}
      public string Lesson {get; set;}
      public double Score {get; set;}
    }

}
