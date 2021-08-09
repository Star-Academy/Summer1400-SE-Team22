using Microsoft.EntityFrameworkCore;

namespace SampleLibrary
{
    public class SearchContext  : DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options)
        {
        }

        public DbSet<InvertedIndex> InvertedIndexes { get; set; }
        public DbSet<WordInfo> WordInfos { get; set; }
    }
}