using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleLibrary
{
    public class Searcher
    {
        public Searcher(SearchContext searchContext, InvertedIndex invertedIndex)
        {
            SearchContext = searchContext;
            InvertedIndex = invertedIndex;
        }

        public InvertedIndex InvertedIndex { get; }
        private SearchContext SearchContext { get; }

        public List<WordInfo> Search(string searchingExpression)
        {
            var searchingExpressionLowered = searchingExpression.ToLower();
            var allCandidates =
                InstantiateRequiredLists(searchingExpressionLowered, out var words,
                    out var plusWords, out var minusWords);

            OperatorWordsHandler.IsolatePlusAndMinusWords(words, plusWords, minusWords);

            if (FindFirstNonStopWord(words, out var navigatingIndex))
            {
                return new List<WordInfo>();
            }

            var candidates = FindCandidates(words, navigatingIndex);
            if (candidates.Count == 0)
            {
                return candidates;
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
                ignoredCounter = 0;
            }

            OperatorWordsHandler.HandlePlusWords(allCandidates, plusWords, this);
            return OperatorWordsHandler.HandlePlusAndMinusWords(allCandidates, candidates, minusWords, this);
        }

        private List<WordInfo> FindCandidates(IReadOnlyList<string> words, int navigatingIndex)
        {
            ICollection<WordInfo> candidates = SearchForAWord(words[navigatingIndex]);

            return new List<WordInfo>(candidates);
        }

        private static Dictionary<string, WordInfo> InstantiateRequiredLists(string searchingExpression,
            out List<string> words, out List<string> plusWords,
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

                foreach (var wordInfo in demo.Where(wordInfo => wordInfo.FileName == candidate.FileName
                                                                && candidate.Position + ignoredCounter + 1 ==
                                                                wordInfo.Position))
                {
                    candidates[j] = wordInfo;
                    isExist = true;
                    break;
                }

                if (!isExist)
                {
                    candidates.RemoveAt(j);
                }
            }
        }

        public void PrintResults(List<WordInfo> candidates)
        {
            if (candidates == null)
            {
                Console.WriteLine("there is no match!");
                return;
            }

            candidates.ForEach(candidate =>
                Console.WriteLine("File name: " + candidate.FileName + " ApproximatePosition: " +
                                  +(candidate.Position - candidates.Count + 1)));
        }

        public List<WordInfo> SearchForAWord(string word)
        {
            word = word.ToLower();
            var wordIds = SearchContext.Words.Where(x => x.WordContent == word).Select(x => x.WordId);
            return SearchContext.WordInfos.Where(y => wordIds.Contains(y.WordId)).ToList();
        }

    }
}