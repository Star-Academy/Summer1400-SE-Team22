using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class Word
    {
        [Key]
        public string WordContent;
        public List<WordDocument> AllWordOwners { get; set; } = new List<WordDocument>();

        public Word(string wordContent)
        {
            WordContent = wordContent;
        }

        public Word()
        {
        }
    }
}