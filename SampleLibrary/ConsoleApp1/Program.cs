﻿using System;
using System.Threading.Tasks;
using SampleLibrary;

namespace ConsoleApp1
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var context = new SearchContext())
            {
                context.Database.EnsureCreated();

                Searcher.SearchContext = context;
                var invertedIndex = new InvertedIndex();
                Searcher.InvertedIndex = invertedIndex;
                invertedIndex.IndexAllFiles("EnglishData");

                // foreach (var x in invertedIndex.Words)
                // {
                // Console.WriteLine(x.WordContent);
                // context.Words.Add(x);
                // }


                // var word = new Word()
                // {
                //     // WordId = 1,
                //     WordContent =  "hello"
                // };
                // var wordInfo = new WordInfo()
                // {
                //     // WordInfoId = 4,
                //     FileName = "pei",
                //     Position = 5,
                //     // WordId = 1,
                //     Word = word
                // };
                // word.AllWordOwners.Add(wordInfo);
                // context.Add(word);

                Console.WriteLine("-------------------------");

                context.SaveChanges();
                // await context.SaveChangesAsync();

                // var author = context.Words.Single(a => a.WordContent == "hello");
                // Console.WriteLine(author.WordContent);
            }
        }


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