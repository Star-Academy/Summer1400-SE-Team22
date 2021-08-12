using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SampleLibrary
{
    public class SearchContext  : DbContext
    {
        // public DbSet<Document> Documents { get; set; }
        public DbSet<WordInfo> WordInfos { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=invertedIndex;Integrated Security=True;User ID=;Password=745910;Pooling=False;Application Name=sqlops-connection-string");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<WordInfo>().ToTable("WordInfos");
            // modelBuilder.Entity<Word>().ToTable("Words");

            // modelBuilder.Entity<WordDocument>()
                // .HasKey(t => new { t.WordContent, t.DocumentName });

            // modelBuilder.Entity<WordDocument>()
                // .HasOne(bc => bc.Word)
                // .WithMany(b => b.AllWordOwners)
                // .HasForeignKey(bc => bc.WordContent);

            // modelBuilder.Entity<WordDocument>()
                // .HasOne(bc => bc.Document)
                // .WithMany(c => c.AllDocumentWords)
                // .HasForeignKey(bc => bc.DocumentName);
                modelBuilder.Entity<Word>(entity =>
                {
                    entity.Property(e => e.WordContent)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);
                });

                modelBuilder.Entity<WordInfo>(entity =>
                {
                    entity.Property(e => e.FileName)
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.HasOne(d => d.Word)
                        .WithMany(p => p.AllWordOwners)
                        .HasForeignKey(d => d.WordContent)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_WordInfo_Word");
                });
        }



    }
}