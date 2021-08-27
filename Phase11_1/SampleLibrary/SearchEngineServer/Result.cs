using System.Collections.Generic;
using System.Linq;
using SampleLibrary;

namespace SearchEngineServer
{
    public class Result
    {
        public string Name { get; }
        public int Position { get; }

        private Result(string name, int position)
        {
            Name = name;
            Position = position;
        }

        public static IEnumerable<Result> BuildResults(IEnumerable<WordInfo> wordInfos)
        {
            var results = new List<Result>();
            wordInfos.ToList().ForEach(wordInfo => results.Add(new Result(wordInfo.FileName, wordInfo.Position)));
            return results;
        }
    }
}