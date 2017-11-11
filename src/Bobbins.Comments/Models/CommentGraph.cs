using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Bobbins.Comments.Models
{
    public static class CommentGraph
    {
        public static IEnumerable<CommentDto> Nest(IEnumerable<CommentDto> source)
        {
            var dict = source.ToDictionary(c => c.Id);

            foreach (var comment in dict.Values.Where(c => c.ReplyToId.HasValue))
            {
                Debug.Assert(comment.ReplyToId != null, "comment.ReplyToId != null");
                if (dict.TryGetValue(comment.ReplyToId.Value, out var parent))
                {
                    if (parent.Replies == null)
                    {
                        parent.Replies = new List<CommentDto>();
                    }
                    parent.Replies.Add(comment);
                }
            }

            return dict.Values.Where(c => !c.ReplyToId.HasValue);
        }
    }
}