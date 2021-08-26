namespace SampleLibrary
{
    public class WordInfo
    {
        private readonly string _fileName;
        private readonly int _position;

        public WordInfo(string fileName, int position)
        {
            _fileName = fileName;
            _position = position;
        }

        public string GetFileName()
        {
            return _fileName;
        }

        public int GetPosition()
        {
            return _position;
        }
    }
}