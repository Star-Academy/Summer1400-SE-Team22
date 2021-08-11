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

        public Dictionary<string, List<WordInfo>> Index { get; } = new Dictionary<string, List<WordInfo>>();
        public List<Word> Words { get; set; } = new List<Word>();

        public void IndexAllFiles(string folderAddress)
        {
            var listOfFiles = Directory.GetFiles(folderAddress);

            Console.WriteLine("indexing...");
            foreach (var fileAddress in listOfFiles)
            {
                IndexFile(fileAddress);
            }

            Console.WriteLine("indexing completed.");
        }

        private void IndexFile(string fileAddress)
        {
            var position = 0;
            var text = FileReader.ReadFileContent(fileAddress);

            // var document = new Document(fileAddress);
            // Searcher.SearchContext.Documents.Add(document);

            foreach (var word in text.Split(' '))
            {
                var wordCopy = word.ToLower();

                position++;
                if (StopWords.Contains(wordCopy)) continue;

                // var wordObj = GetWord(word);
                // if (wordObj == null)
                // {
                    // wordObj = new Word(word);
                    // wordObj.AllWordOwners.Add(document);
                    // Searcher.SearchContext.Words.Add(wordObj);
                    // Words.Add(wordObj);
                // }

                // var wordDocument = new WordDocument(wordCopy, fileAddress, wordObj, document);
                // document.AllDocumentWords.Add(wordDocument);

                if (!Index.ContainsKey(wordCopy))
                {
                    Index.Add(wordCopy, new List<WordInfo>());
                }

                Index[wordCopy].Add(new WordInfo(fileAddress, position));
            }
        }

        // public Word GetWord(string word)
        // {
            // foreach (var w in Words)
            // {
                // if (w.WordContent == word)
                // {
                    // return w;
                // }
            // }
            // return null;
        // }
    }
}