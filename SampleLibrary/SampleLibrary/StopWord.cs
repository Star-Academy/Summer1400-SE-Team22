using System.ComponentModel.DataAnnotations;

namespace SampleLibrary
{
    public class StopWord
    {
        [Key]
        public string value { get; set; }

        public StopWord(string value)
        {
            this.value = value;
        }
    }
}