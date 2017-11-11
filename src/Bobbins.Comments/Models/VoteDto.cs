using System.ComponentModel.DataAnnotations;
using Bobbins.Comments.Data;

namespace Bobbins.Comments.Models
{
    public class VoteDto
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        public int Value { get; set; }

        [MaxLength(16)]
        public string User { get; set; }
    }
}