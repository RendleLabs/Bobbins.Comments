using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bobbins.Comments.Data;
using Bobbins.Comments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bobbins.Comments.Controllers
{
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly CommentContext _context;

        private readonly IMapper _mapper;

        public CommentsController(CommentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("for-link/{linkId}")]
        public async Task<IActionResult> Get(int linkId, CancellationToken ct)
        {
            var data = await _context.Comments
                .Where(c => c.LinkId == linkId)
                .ToListAsync(cancellationToken: ct)
                .ConfigureAwait(false);
            
            if (data.Count == 0) return Ok(Enumerable.Empty<CommentDto>());

            var comments = _mapper.Map<List<CommentDto>>(data);

            return Ok(CommentGraph.Nest(comments));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id, CancellationToken ct)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id, ct)
                .ConfigureAwait(false);

            if (comment is null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentDto commentDto, CancellationToken ct)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            
            _context.Comments.Add(comment);
            
            await _context.SaveChangesAsync(ct).ConfigureAwait(false);

            await _context.IncrementReplyCountAsync(commentDto.ReplyToId.GetValueOrDefault(), ct);
            
            return CreatedAtAction("GetComment", new {id = comment.Id}, _mapper.Map<CommentDto>(comment));
        }
    }
}
