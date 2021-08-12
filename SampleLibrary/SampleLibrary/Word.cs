using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class Word
    {
        public int WordId { get; set; }
        [StringLength(255)]
        public string WordContent { get; set; }
        public ICollection<WordInfo> AllWordOwners { get; set; } = new List<WordInfo>();

        public Word(string wordContent)
        {
            WordContent = wordContent;
        }

        public Word()
        {
        }
    }
}