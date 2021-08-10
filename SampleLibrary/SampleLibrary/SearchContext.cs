using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SampleLibrary
{
    public class SearchContext  : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=master;Integrated Security=True;User ID=;Password=745910;Pooling=False;Application Name=sqlops-connection-string");
        }

        public Word GetWord(string word)
        {
            return Enumerable.FirstOrDefault(Words, w => w.Content == word);
        }

    }
}