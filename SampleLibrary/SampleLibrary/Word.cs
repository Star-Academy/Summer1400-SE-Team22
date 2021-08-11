using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class Word
    {
        [Key]
        public string WordContent { get; set; }
        public List<WordInfo> AllWordOwners { get; set; } = new List<WordInfo>();

        public Word(string wordContent)
        {
            WordContent = wordContent;
        }

        public Word()
        {
        }
    }
}