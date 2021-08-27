using System;
using System.Collections.Generic;
using System.Linq;

namespace Summer1400_SE_Team22
{
    public class ResultPrinter
    {
        public static void PrintResults(IEnumerable<Student> students, int printingStudentsCount)
        {
            students.ToList()
                .OrderByDescending(student => student.GPA)
                .Take(printingStudentsCount).ToList()
                .ForEach(s => Console.WriteLine($"-> name: {s.FirstName} {s.LastName}, GPA: {s.GPA:N2}"));
        }
    }
}