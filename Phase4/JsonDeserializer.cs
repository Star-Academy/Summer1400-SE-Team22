using System.IO;
using System.Text.Json;

namespace Summer1400_SE_Team22
{
    public static class JsonDeserializer
    {
        public static T Deserialize<T>(string jsonPath)
        {
            var jsonFileContent = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<T>(jsonFileContent);
        }
    }
}