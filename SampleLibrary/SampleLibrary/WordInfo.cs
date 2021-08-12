using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class WordInfo
    {
        [Key] public int Id { get; set; }
        public string WordContent { get; set; }
        public string FileName { get; set; }
        public int Position { get; set; }
        public Word Word { get; set; }


        public WordInfo(string fileName, int position)
        {
            FileName = fileName;
            Position = position;
        }

        public WordInfo()
        {
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