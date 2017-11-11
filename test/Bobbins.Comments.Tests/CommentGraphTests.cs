using System;
using System.Collections.Generic;
using System.Linq;
using Bobbins.Comments.Models;
using Xunit;

namespace Bobbins.Comments.Tests
{
    public class CommentGraphTests
    {
        [Fact]
        public void NestsComments()
        {
            var source = new[]
            {
                new CommentDto {Id = 1},
                new CommentDto {Id = 2},
                new CommentDto {Id = 3, ReplyToId = 1},
                new CommentDto {Id = 4, ReplyToId = 2},
                new CommentDto {Id = 5, ReplyToId = 3}
            };

            var actual = CommentGraph.Nest(source).ToArray();
            
            Assert.Equal(2, actual.Length);
        }
    }
}
