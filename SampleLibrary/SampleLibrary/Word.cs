using System.Collections.Generic;

namespace SampleLibrary
{
    public class Word
    {
        public string Content;
        public List<Document> AllWordOwners { get; set; } = new List<Document>();

        public Word(string content)
        {
            Content = content;
        }
    }
}