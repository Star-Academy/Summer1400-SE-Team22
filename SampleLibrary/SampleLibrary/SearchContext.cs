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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Word>()
                .HasData(new Word {});

            // modelBuilder
            //     .Entity<Document>()
            //     .HasData(new Document { TagId = "ef" });

            // modelBuilder
            //     .Entity<Word>()
            //     .HasMany(p => p.AllWordOwners)
            //     .WithMany(p => p)
            //     .UsingEntity(j => j.HasData(new { PostsPostId = 1, TagsTagId = "ef" }));
        }

        public Word GetWord(string word)
        {
            return Enumerable.FirstOrDefault(Words, w => w.Content == word);
        }

    }
}