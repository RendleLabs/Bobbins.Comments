using System;
using System.Threading.Tasks;
using Bobbins.Comments.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bobbins.Data.IntegrationTests
{
    public class CommentContextExtensionsTests
    {
        private const string LocalPostgres = "Host=localhost;Database=comments;Username=bobbins;Password=secretsquirrel";
        
        [Fact]
        public async Task IncrementReplyCount_AddsOneTo_ReplyCount()
        {
            var comment = new Comment
            {
                Text = "IncrementCommentCountTest",
                User = "bob"
            };

            using (var context = new CommentContext(Options))
            {
                context.Comments.Add(comment);
                await context.SaveChangesAsync();

                await context.AddUpVoteAsync(comment.Id);
                
                await context.Entry(comment).ReloadAsync();

                Assert.Equal(1, comment.UpVoteCount);
            }
        }
        
        [Fact]
        public async Task AddUpVote_AddsOneTo_UpVoteCount()
        {
            var comment = new Comment
            {
                Text = "IncrementCommentCountTest",
                User = "bob"
            };

            using (var context = new CommentContext(Options))
            {
                context.Comments.Add(comment);
                await context.SaveChangesAsync();

                await context.AddUpVoteAsync(comment.Id);
                
                await context.Entry(comment).ReloadAsync();

                Assert.Equal(1, comment.UpVoteCount);
            }
        }

        [Fact]
        public async Task AddDownVote_AddsOneTo_DownVoteCount()
        {
            var comment = new Comment
            {
                Text = "IncrementCommentCountTest",
                User = "bob"
            };

            using (var context = new CommentContext(Options))
            {
                context.Comments.Add(comment);
                await context.SaveChangesAsync();

                await context.AddDownVoteAsync(comment.Id);
                
                await context.Entry(comment).ReloadAsync();

                Assert.Equal(1, comment.DownVoteCount);
            }
        }
        
        private static DbContextOptions<CommentContext> Options =>
            new DbContextOptionsBuilder<CommentContext>().UseNpgsql(LocalPostgres).Options;
    }
}
