using Microsoft.EntityFrameworkCore;

namespace QuotingDojoWithEFWoutTagH.Models
{
    public partial class QuotingContext : DbContext
    {

        public QuotingContext(DbContextOptions<QuotingContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>(entity =>
            {
                entity.Property( e => e.Name).IsRequired();
                entity.Property( e => e.QuoteText).IsRequired();
            });
        }
        public virtual DbSet<Quote> Quote { get; set; }
    }
}