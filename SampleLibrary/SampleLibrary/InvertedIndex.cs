using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleLibrary
{
    public class InvertedIndex
    {
        public List<string> StopWords { get; } =
            FileReader.ReadFileContent("stopWords.txt").Split(',').ToList();

        public Dictionary<string, List<WordInfo>> Index { get; } = new();
        public List<Word> Words { get; set; } = new();

        public void IndexAllFiles(string folderAddress)
        {
            var listOfFiles = Directory.GetFiles(folderAddress);

            Console.WriteLine("indexing...");
            foreach (var fileAddress in listOfFiles) IndexFile(fileAddress);

            Console.WriteLine("indexing completed.");
        }

        private void IndexFile(string fileAddress)
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
            position++;
            if (StopWords.Contains(word) || word.Length > 80) return;

            if (!Index.ContainsKey(word)) Index.Add(word, new List<WordInfo>());

            var wordInfo = new WordInfo(fileAddress, position);
            Index[word].Add(wordInfo);

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