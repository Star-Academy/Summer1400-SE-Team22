using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleLibrary
{
    public class Searcher
    {
        public static InvertedIndex InvertedIndex { get; set; }
        public static SearchContext SearchContext { get; set; }

        public static void Run(string folderAddress)
        {
            InvertedIndex = new InvertedIndex();
            InvertedIndex.IndexAllFiles(folderAddress);
            while (true)
            {
                Console.WriteLine("enter a word for search:");
                var input = Console.ReadLine();
                if (input == "exit") return;
                PrintResults(Search(input));
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public static List<WordInfo> Search(string searchingExpression)
        {
            var allCandidates = new Dictionary<string, WordInfo>();
            searchingExpression = searchingExpression.ToLower();
            var words = new List<string>(searchingExpression.Split(' '));
            var plusWords = new List<string>();
            var minusWords = new List<string>();

            IsolatePlusAndMinusWords(words, plusWords, minusWords);

            var navigatingIndex = 0;
            try
            {
                while (InvertedIndex.StopWords.Contains(words[navigatingIndex])) navigatingIndex++;
            }
            catch (Exception)
            {
                Console.WriteLine("please try a different keyword for your search!");
                return null;
            }

            List<WordInfo> candidates;
            try
            {
                candidates = new List<WordInfo>(SearchForAWord(words[navigatingIndex]));
            }
            catch (Exception)
            {
                return null;
            }

            var ignoredCounter = 0;
            for (navigatingIndex += 1; navigatingIndex < words.Count; navigatingIndex++)
            {
                var word = words[navigatingIndex];
                if (InvertedIndex.StopWords.Contains(word))
                {
                    ignoredCounter++;
                    continue;
                }

                var demo = SearchForAWord(words[navigatingIndex]);
                ReduceResultsToMatchSearch(candidates, ignoredCounter, demo);
                HandlePlusWords(allCandidates, plusWords);
                ignoredCounter = 0;
            }

            SumResultsWithPlusWords(allCandidates, candidates);
            candidates = new List<WordInfo>(allCandidates.Values);
            DeleteMinusWordsFromCandidates(minusWords, candidates);
            return candidates;
        }

        private static void ReduceResultsToMatchSearch(IList<WordInfo> candidates, int ignoredCounter,
            IReadOnlyCollection<WordInfo> demo)
        {
            for (var j = candidates.Count - 1; j >= 0; j--)
            {
                var candidate = candidates[j];
                var isExist = false;

                foreach (var wordInfo in demo.Where(wordInfo => wordInfo.GetFileName() == candidate.GetFileName()
                                                                && candidate.GetPosition() + ignoredCounter + 1 ==
                                                                wordInfo.GetPosition()))
                {
                    candidates[j] = wordInfo;
                    isExist = true;
                    break;
                }

                if (!isExist)
                    candidates.RemoveAt(j);
            }
        }

        private static void SumResultsWithPlusWords(IDictionary<string, WordInfo> allCandidates,
            IEnumerable<WordInfo> candidates)
        {
            foreach (var candidate in
                candidates.Where(candidate => !allCandidates.ContainsKey(candidate.GetFileName())))
                allCandidates.Add(candidate.GetFileName(), candidate);
        }

        private static void HandlePlusWords(IDictionary<string, WordInfo> allCandidates, List<string> plusWords)
        {
            foreach (var wordInfo in plusWords.SelectMany(plusWord =>
                SearchForAWord(plusWord).Where(wordInfo => !allCandidates.ContainsKey(wordInfo.GetFileName()))))
                allCandidates.Add(wordInfo.GetFileName(), wordInfo);
        }

        public static void PrintResults(List<WordInfo> candidates)
        {
            if (candidates == null)
            {
                Console.WriteLine("there is no match!");
                return;
            }

            foreach (var candidate in candidates)
                Console.WriteLine("File name: " + candidate.GetFileName()
                                                + " ApproximatePosition: " +
                                                +(candidate.GetPosition() - candidates.Count + 1));
        }

        private static void IsolatePlusAndMinusWords(IList<string> words, ICollection<string> plusWords,
            ICollection<string> minusWords)
        {
            for (var i = words.Count - 1; i >= 0; i--)
            {
                var word = words[i];
                if (word.StartsWith("+"))
                {
                    plusWords.Add(word.Substring(1));
                    words.RemoveAt(i);
                }
                else if (word.StartsWith("-"))
                {
                    minusWords.Add(word.Substring(1));
                    words.RemoveAt(i);
                }
            }
        }

        private static List<WordInfo> SearchForAWord(string word)
        {
            word = word.ToLower();
            return InvertedIndex.Index[word];
        }

        private static void DeleteMinusWordsFromCandidates(IEnumerable<string> minusWords, IList<WordInfo> candidates)
        {
            foreach (var toBeRemovedDoc in minusWords.Select(SearchForAWord)
                .SelectMany(toBeRemovedDocs => toBeRemovedDocs))
                for (var j = candidates.Count - 1; j >= 0; j--)
                    if (candidates[j].GetFileName() == toBeRemovedDoc.GetFileName())
                        candidates.RemoveAt(j);
        }
    }
}