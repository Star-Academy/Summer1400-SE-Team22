using System.Collections.Generic;
using System.Linq;

namespace Summer1400_SE_Team22
{
    public static class GPACalculator
    {
        public static void CalculateGPA(IEnumerable<LessonScore> scores, IReadOnlyList<Student> students)
        {
            scores.ToList()
                .ForEach(l => students[l.StudentNumber - 1].AddScore(l.Score));

            students.ToList()
                .ForEach(s => s.GPA = s.Scores.Average());
        }
    }
}