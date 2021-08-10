﻿using System.Data;
using Microsoft.Data.SqlClient;
using SampleLibrary;

namespace ConsoleApp1
{
    class Program
    {
        private static void Main()
        {
            using var context = new SearchContext();
            Searcher.SearchContext = context;
            var invertedIndex = new InvertedIndex();
            Searcher.InvertedIndex = invertedIndex;
            invertedIndex.IndexAllFiles("EnglishData");
            // foreach(var s in invertedIndex.StopWords)
            //     context.StopWords.Add(new StopWord(s));
            // context.SaveChanges();

        }
    }

    // string str = @"Data Source=localhost;Initial Catalog=master;Integrated Security=True;User ID=;Password=745910;Pooling=False;Application Name=sqlops-connection-string" ;
    // SqlConnection con = new SqlConnection(str);
    // SqlCommand cmd = new SqlCommand();
    // SqlDataReader r;
    //
    // cmd.CommandText = @"CREATE TABLE Student2
    //         (
    //             StudentNumber VARCHAR(8) NOT NULL,
    //             Grade FLOAT(2),
    //             FirstName NVARCHAR(20) NOT NULL,
    //             LastName NVARCHAR(20) NOT NULL,
    //             IsMale BIT NOT NULL,
    //             DateOfBirth DATETIME NOT NULL,
    //             LeftUnitsCount INT NOT NULL
    //         );";
    //
    // cmd.CommandType = CommandType.Text;
    // cmd.Connection = con;
    //
    // con.Open();
    //
    // r = cmd.ExecuteReader();
    //
    // con.Close();
}