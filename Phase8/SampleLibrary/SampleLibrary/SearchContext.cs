using Microsoft.EntityFrameworkCore;

namespace SampleLibrary
{
    public class SearchContext : DbContext
    {
        public DbSet<WordInfo> WordInfos { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=localhost;Initial Catalog=invertedIndex;Integrated Security=True;Pooling=False;Application Name=sqlops-connection-string");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.WordContent)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WordInfo>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.AllWordOwners)
                    .HasForeignKey(d => d.WordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WordInfo_Word");
            });
        }
    }
}