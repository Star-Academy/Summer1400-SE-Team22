namespace SampleLibrary
{
    public class WordDocument
    {
        public WordDocument(string wordContent, string documentName, Word word, Document document)
        {
            WordContent = wordContent;
            DocumentName = documentName;
            Word = word;
            Document = document;
        }

        public WordDocument()
        {
        }

        public string WordContent { get; set; }
        public string DocumentName { get; set; }
        public Word Word { get; set; }
        public Document Document { get; set; }
    }
}