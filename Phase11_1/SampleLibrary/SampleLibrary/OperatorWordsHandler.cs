using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleLibrary
{
    public static class OperatorWordsHandler
    {
        private static void SumResultsWithPlusWords(IDictionary<string, WordInfo> allCandidates,
            IEnumerable<WordInfo> candidates)
        {
            candidates.ToList().ForEach(candidate =>
            {
                try
                {
                    allCandidates.Add(candidate.FileName, candidate);
                }
                catch (Exception)
                {
                    // ignored
                }
            });
        }

        public static List<WordInfo> HandlePlusAndMinusWords(Dictionary<string, WordInfo> allCandidates,
            List<WordInfo> candidates, IEnumerable<string> minusWords, Searcher searcher)
        {
            SumResultsWithPlusWords(allCandidates, candidates);
            candidates = new List<WordInfo>(allCandidates.Values);
            DeleteMinusWordsFromCandidates(minusWords, candidates, searcher);
            return candidates;
        }

        private static void DeleteMinusWordsFromCandidates(IEnumerable<string> minusWords, IList<WordInfo> candidates,
            Searcher searcher)
        {
            minusWords.Select(searcher.SearchForAWord)
                .SelectMany(toBeRemovedDocs => toBeRemovedDocs).ToList().ForEach(toBeRemovedDoc =>
                {
                    for (var j = candidates.Count - 1; j >= 0; j--) {
                        if (candidates[j].FileName == toBeRemovedDoc.FileName)
                        {
                            candidates.RemoveAt(j);
                        }}
                });
        }

        public static void IsolatePlusAndMinusWords(IList<string> words, ICollection<string> plusWords,
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

        public static void HandlePlusWords(IDictionary<string, WordInfo> allCandidates, List<string> plusWords,
            Searcher searcher)
        {
            plusWords.SelectMany(plusWord => searcher.SearchForAWord(plusWord)
                    .Where(wordInfo => !allCandidates.ContainsKey(wordInfo.FileName)))
                .ToList().ForEach(wordInfo => allCandidates.Add(wordInfo.FileName, wordInfo));
        }
    }
}