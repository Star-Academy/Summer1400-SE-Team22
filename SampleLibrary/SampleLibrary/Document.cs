using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleLibrary
{
    public class Document
    {
        [Key]
        public string name;

        public List<Word> AllDocumentWords { get; set; } = new List<Word>();

        public Document(string name)
        {
            this.name = name;
        }

        public Document()
        {
        }
    }
}