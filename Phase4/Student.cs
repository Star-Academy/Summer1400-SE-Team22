using System.Collections.Generic;

namespace Summer1400_SE_Team22
{
    public class Student
    {
        public readonly List<double> Scores = new List<double>();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double GPA { get; set; }

        public void AddScore(double score)
        {
            Scores.Add(score);
        }
    }
}