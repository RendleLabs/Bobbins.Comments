using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Comments.Data
{
    [PublicAPI]
    public class CommentContext : DbContext
    {
        public CommentContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Vote>()
                .HasIndex(v => new {v.CommentId, v.User})
                .IsUnique();
        }
    }
}