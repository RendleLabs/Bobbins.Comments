using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Comments.Data
{
    public static class CommentContextExtensions
    {
        public static async Task<int> IncrementReplyCountAsync(this CommentContext context, int commentId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT increment_reply_count({0})", new object[] {commentId}, ct);
        }
        
        public static async Task<int> AddUpVoteAsync(this CommentContext context, int commentId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT add_up_vote({0})", new object[] {commentId}, ct);
        }
        
        public static async Task<int> AddDownVoteAsync(this CommentContext context, int commentId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT add_down_vote({0})", new object[] {commentId}, ct);
        }
        
        public static async Task<int> ChangeUpVoteAsync(this CommentContext context, int commentId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT change_up_vote({0})", new object[] {commentId}, ct);
        }
        
        public static async Task<int> ChangeDownVoteAsync(this CommentContext context, int commentId, CancellationToken ct = default)
        {
            return await context.Database.ExecuteSqlCommandAsync("SELECT change_down_vote({0})", new object[] {commentId}, ct);
        }
    }
}