using System.ComponentModel.DataAnnotations;

namespace Bobbins.Comments.Data
{
    public class Vote
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        public int Value { get; set; }

        [MaxLength(16)]
        public string User { get; set; }
    }
}