using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class Document
    {
        // public List<WordDocument> AllDocumentWords { get; set; } = new List<WordDocument>();

        public Document(string documentName)
        {
            DocumentName = documentName;
        }

        public Document()
        {
        }

        [Key] public string DocumentName { get; set; }
    }
}