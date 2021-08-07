using System;
using System.Collections.Generic;

namespace SampleLibrary
{
    public class Searcher
    {
        public static InvertedIndex InvertedIndex { get; set; }

        public void Run(string folderAddress)
        {
            InvertedIndex = new InvertedIndex();
            InvertedIndex.IndexAllFiles(folderAddress);
            while (true)
            {
                Console.WriteLine("enter a word for search:");
                string input = Console.ReadLine();
                if (input == "exit") return;
                PrintResults(Search(input));
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public List<WordInfo> Search(string searchingExpression)
        {
            Dictionary<string, WordInfo> allCandidates = new Dictionary<string, WordInfo>();
            searchingExpression = searchingExpression.ToLower();
            List<string> words = new List<string>(searchingExpression.Split(' '));
            List<string> plusWords = new List<string>();
            List<string> minusWords = new List<string>();

            IsolatePlusAndMinusWords(words, plusWords, minusWords);

            int navigatingIndex = 0;
            try
            {
                while (InvertedIndex.StopWords.Contains(words[navigatingIndex]))
                {
                    navigatingIndex++;
                }
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

            int ignoredCounter = 0;
            for (navigatingIndex += 1; navigatingIndex < words.Count; navigatingIndex++)
            {
                string word = words[navigatingIndex];
                if (InvertedIndex.StopWords.Contains(word))
                {
                    ignoredCounter++;
                    continue;
                }

                List<WordInfo> demo = SearchForAWord(words[navigatingIndex]);
                ReduceResultsToMatchSearch(candidates, ignoredCounter, demo);
                HandlePlusWords(allCandidates, plusWords);
                ignoredCounter = 0;
            }

            SumResultsWithPlusWords(allCandidates, candidates);
            candidates = new List<WordInfo>(allCandidates.Values);
            DeleteMinusWordsFromCandidates(minusWords, candidates);
            return candidates;
        }

        private static void ReduceResultsToMatchSearch(List<WordInfo> candidates, int ignoredCounter, List<WordInfo> demo)
        {
            for (int j = candidates.Count - 1; j >= 0; j--)
            {
                WordInfo candidate = candidates[j];
                bool isExist = false;

                foreach (WordInfo wordInfo in
                    demo)
                {
                    if (wordInfo.GetFileName() == candidate.GetFileName()
                        && candidate.GetPosition() + ignoredCounter + 1 == wordInfo.GetPosition())
                    {
                        candidates[j] = wordInfo;
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                    candidates.RemoveAt(j);
            }
        }

        private static void SumResultsWithPlusWords(Dictionary<string, WordInfo> allCandidates, List<WordInfo> candidates)
        {
            foreach (var candidate in
                candidates)
            {
                if (!allCandidates.ContainsKey(candidate.GetFileName()))
                {
                    allCandidates.Add(candidate.GetFileName(), candidate);
                }
            }
        }

        private void HandlePlusWords(Dictionary<string, WordInfo> allCandidates, List<string> plusWords)
        {
            foreach (string plusWord in plusWords)
            {
                foreach (WordInfo wordInfo in
                    SearchForAWord(plusWord))
                {
                    if (!allCandidates.ContainsKey(wordInfo.GetFileName()))
                        allCandidates.Add(wordInfo.GetFileName(), wordInfo);
                }
            }
        }

        public static void PrintResults(List<WordInfo> candidates)
        {
            if (candidates == null)
            {
                Console.WriteLine("there is no match!");
                return;
            }

            foreach (WordInfo candidate in
                candidates)
                Console.WriteLine("File name: " + candidate.GetFileName()
                                                + " ApproximatePosition: " +
                                                +(candidate.GetPosition() - candidates.Count + 1));
        }

        private static void IsolatePlusAndMinusWords(List<string> words, List<string> plusWords, List<string> minusWords)
        {
            for (int i = words.Count - 1; i >= 0; i--)
            {
                string word = words[i];
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

        private static void DeleteMinusWordsFromCandidates(List<string> minusWords, List<WordInfo> candidates)
        {
            foreach (string minusWord in
                minusWords)
            {
                List<WordInfo> toBeRemovedDocs = SearchForAWord(minusWord);
                foreach (WordInfo toBeRemovedDoc in
                    toBeRemovedDocs)
                {
                    for (int j = candidates.Count - 1; j >= 0; j--)
                    {
                        if (candidates[j].GetFileName() == (toBeRemovedDoc.GetFileName()))
                            candidates.RemoveAt(j);
                    }
                }
            }
        }
    }
}