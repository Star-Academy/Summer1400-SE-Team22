namespace SampleLibrary
{
    public class WordDocument
    {
        public string WordContent;
        public string DocumentName;
        public Word Word;
        public Document Document;

        public WordDocument(string wordContent, string documentName, Word word, Document document)
        {
            WordContent = wordContent;
            DocumentName = documentName;
            Word = word;
            Document = document;
        }
    }
}