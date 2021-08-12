using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleLibrary;

namespace ConsoleApp1
{
    class Program
    {
        private static void Main()
        {
            using (var context = new SearchContext())
            {

                context.Database.EnsureCreated();

                Searcher.SearchContext = context;
                var invertedIndex = new InvertedIndex();
                Searcher.InvertedIndex = invertedIndex;
                invertedIndex.IndexAllFiles("EnglishData");

                foreach (var x in invertedIndex.Words)
                {
                    Console.WriteLine(x.WordContent);
                    context.Add(x);
                }
                Console.WriteLine("-------------------------");

                context.SaveChanges();

                // var author = context.Documents.First();
                // Console.WriteLine(author.DocumentName);
                var author = context.Words.Single(a => a.WordContent == "hello");
                Console.WriteLine(author.WordContent);
            }
        }


        // private static void Main()
        // {
        //     var dept = new Department()
        //     {
        //         Name = "Designing"
        //     };
        //
        //     using (var context = new CompanyContext())
        //     {
        //         // context.Add(dept);
        //         context.Department.Add(dept);
        //         context.SaveChanges();
        //     }
        // }

        // private static void Main1()
        // {
        //     string str = @"Data Source=localhost\SQLExpress,1433;Database=first;Integrated Security=sspi;";
        //     SqlConnection con = new SqlConnection(str);
        //     SqlCommand cmd = new SqlCommand();
        //     SqlDataReader r;
        //     cmd.CommandText = @"CREATE TABLE Student4
        //     (
        //         StudentNumber VARCHAR(8) NOT NULL,
        //         Grade FLOAT(2),
        //         FirstName NVARCHAR(20) NOT NULL,
        //         LastName NVARCHAR(20) NOT NULL,
        //         IsMale BIT NOT NULL,
        //         DateOfBirth DATETIME NOT NULL,
        //         LeftUnitsCount INT NOT NULL
        //     );";
        //     cmd.CommandType = CommandType.Text;
        //     cmd.Connection = con;
        //     con.Open();
        //     r = cmd.ExecuteReader();
        //     con.Close();
        // }
    }
}