namespace SampleLibrary
{
    public class WordInfo
    {
        public WordInfo(string fileName, int position)
        {
            FileName = fileName;
            Position = position;
        }

        public WordInfo()
        {
        }

        public int WordInfoId { get; set; }
        public string FileName { get; }
        public int Position { get; }
        public int WordId { get; set; }
        public Word Word { get; set; }
    }
}