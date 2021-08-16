using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleLibrary
{
    public class Searcher
    {
        public InvertedIndex InvertedIndex { get; }
        private SearchContext SearchContext { get; }

        public Searcher(SearchContext searchContext, InvertedIndex invertedIndex)
        {
            SearchContext = searchContext;
            InvertedIndex = invertedIndex;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("enter a word for search:");
                var input = Console.ReadLine();
                if (input == "exit") return;
                PrintResults(Search(input));
                Console.WriteLine("---------------------------------------------------");
            }
        }

        public List<WordInfo> Search(string searchingExpression)
        {
            searchingExpression = searchingExpression.ToLower();
            var allCandidates =
                InstantiateRequiredLists(searchingExpression, out var words, out var plusWords, out var minusWords);

            IsolatePlusAndMinusWords(words, plusWords, minusWords);

            if (FindFirstNonStopWord(words, out var navigatingIndex)) return new List<WordInfo>();

            var candidates = FindCandidates(words, navigatingIndex);
            if (candidates.Count == 0) return candidates;

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

            return HandlePlusAndMinusWords(allCandidates, candidates, minusWords);
        }

        private List<WordInfo> FindCandidates(IReadOnlyList<string> words, int navigatingIndex)
        {
            ICollection<WordInfo> candidates = SearchForAWord(words[navigatingIndex]);

            return new List<WordInfo>(candidates);
        }

        private List<WordInfo> HandlePlusAndMinusWords(Dictionary<string, WordInfo> allCandidates, List<WordInfo> candidates, List<string> minusWords)
        {
            SumResultsWithPlusWords(allCandidates, candidates);
            candidates = new List<WordInfo>(allCandidates.Values);
            DeleteMinusWordsFromCandidates(minusWords, candidates);
            return candidates;
        }

        private static Dictionary<string, WordInfo> InstantiateRequiredLists(string searchingExpression, out List<string> words, out List<string> plusWords,
            out List<string> minusWords)
        {
            var allCandidates = new Dictionary<string, WordInfo>();
            words = new List<string>(searchingExpression.Split(' '));
            plusWords = new List<string>();
            minusWords = new List<string>();
            return allCandidates;
        }

        private bool FindFirstNonStopWord(List<string> words, out int navigatingIndex)
        {
            navigatingIndex = 0;
            try
            {
                while (InvertedIndex.StopWords.Contains(words[navigatingIndex])) navigatingIndex++;
            }
            catch (Exception)
            {
                Console.WriteLine("please try a different keyword for your search!");
                return true;
            }

            return false;
        }

        private void ReduceResultsToMatchSearch(IList<WordInfo> candidates, int ignoredCounter,
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

        private void SumResultsWithPlusWords(IDictionary<string, WordInfo> allCandidates,
            IEnumerable<WordInfo> candidates)
        {
            foreach (var candidate in
                candidates.Where(candidate => !allCandidates.ContainsKey(candidate.GetFileName())))
                allCandidates.Add(candidate.GetFileName(), candidate);
        }

        private void HandlePlusWords(IDictionary<string, WordInfo> allCandidates, List<string> plusWords)
        {
            foreach (var wordInfo in plusWords.SelectMany(plusWord =>
                SearchForAWord(plusWord).Where(wordInfo => !allCandidates.ContainsKey(wordInfo.GetFileName()))))
                allCandidates.Add(wordInfo.GetFileName(), wordInfo);
        }

        public void PrintResults(List<WordInfo> candidates)
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

        private void IsolatePlusAndMinusWords(IList<string> words, ICollection<string> plusWords,
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

        private List<WordInfo> SearchForAWord(string word)
        {
            word = word.ToLower();
            var wordIds = SearchContext.Words.Where(x => x.WordContent == word).Select(x => x.WordId);
            return SearchContext.WordInfos.Where(y => wordIds.Contains(y.WordId)).ToList();
        }

        private void DeleteMinusWordsFromCandidates(IEnumerable<string> minusWords, IList<WordInfo> candidates)
        {
            foreach (var toBeRemovedDoc in minusWords.Select(SearchForAWord)
                .SelectMany(toBeRemovedDocs => toBeRemovedDocs))
                for (var j = candidates.Count - 1; j >= 0; j--)
                    if (candidates[j].GetFileName() == toBeRemovedDoc.GetFileName())
                        candidates.RemoveAt(j);
        }
    }
}