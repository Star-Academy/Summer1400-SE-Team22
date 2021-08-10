using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public Word()
        {
        }
    }
}