using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleLibrary
{
    public class Document
    {
        [Key]
        public string DocumentName;

        public List<WordDocument> AllDocumentWords { get; set; } = new List<WordDocument>();

        public Document(string documentName)
        {
            this.DocumentName = documentName;
        }

        public Document()
        {
        }
    }
}