using System.Collections.Generic;

namespace SampleLibrary
{
    public class Word
    {
        public string Content;
        public List<string> AllWordOwners { get; set; } = new List<string>();

        public Word(string content)
        {
            Content = content;
        }
    }
}