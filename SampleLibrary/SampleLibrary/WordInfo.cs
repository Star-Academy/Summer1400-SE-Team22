namespace SampleLibrary
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public int Position { get; set; }

        public WordInfo(string fileName, int position)
        {
            FileName = fileName;
            Position = position;
        }

        public string GetFileName()
        {
            return FileName;
        }

        public int GetPosition()
        {
            return Position;
        }
    }
}