using System.Linq;
using Bobbins.Comments.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bobbins.Comments.Migrate
{
    [UsedImplicitly]
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<CommentContext>
    {
        private const string LocalPostgres = "Host=localhost;Database=comments;Username=bobbins;Password=secretsquirrel";
        
        private static readonly string MigrationAssemblyName = typeof(DesignTimeContextFactory).Assembly.GetName().Name;

        public CommentContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CommentContext>()
                .UseNpgsql(args.FirstOrDefault() ?? LocalPostgres, b => b.MigrationsAssembly(MigrationAssemblyName));
            return new CommentContext(builder.Options);
        }
    }
}