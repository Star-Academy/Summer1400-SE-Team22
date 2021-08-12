﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleLibrary
{
    public class InvertedIndex
    {
        public List<string> StopWords { get; } =
            FileReader.ReadFileContent("stopWords.txt").Split(',').ToList();

        public List<Word> Words { get; set; } = new List<Word>();

        public void IndexAllFiles(string folderAddress)
        {
            var listOfFiles = Directory.GetFiles(folderAddress);

            Console.WriteLine("indexing...");
            foreach (var fileAddress in listOfFiles) IndexFile(fileAddress);

            Console.WriteLine("indexing completed.");
        }

        public void IndexFile(string fileAddress)
        {
            var position = 0;
            var text = FileReader.ReadFileContent(fileAddress);

            foreach (var word in text.Split(' '))
            {
                HandleIndexing(fileAddress, word.ToLower(), ref position);
            }
        }

        private void HandleIndexing(string fileAddress, string word, ref int position)
        {
            if (Searcher.SearchContext == null)
            {
                Searcher.SearchContext = new SearchContext();
            }

            position++;
            if (StopWords.Contains(word) || word.Length > 80) return;


            var wordInfo = new WordInfo(fileAddress, position);

            var wordObj = GetWord(word);
            if (wordObj == null)
            {
                wordObj = new Word(word);
                Searcher.SearchContext.Words.Add(wordObj);
                Words.Add(wordObj);
            }

            wordInfo.Word = wordObj;
            wordObj.AllWordOwners.Add(wordInfo);
            Searcher.SearchContext.Add(wordObj);
        }

        private Word GetWord(string word)
        {
            return Words.FirstOrDefault(w => w.WordContent == word);
        }
    }
}