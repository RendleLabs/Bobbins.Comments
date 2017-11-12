using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Bobbins.Comments.Data
{
    [PublicAPI]
    public class Comment
    {
        public int Id { get; set; }
        
        public int LinkId { get; set; }
        
        public int? ReplyToId { get; set; }
        
        public string Text { get; set; }
        
        [MaxLength(16)]
        public string User { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public int UpVoteCount { get; set; }

        public int DownVoteCount { get; set; }

        public int ReplyCount { get; set; }

        public List<Vote> Votes { get; set; }
    }
}
