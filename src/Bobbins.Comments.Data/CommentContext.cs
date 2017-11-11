using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Comments.Data
{
    public class CommentContext : DbContext
    {
        public CommentContext([NotNull] DbContextOptions<CommentContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}