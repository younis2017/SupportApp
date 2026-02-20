using Microsoft.AspNetCore.Mvc;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Dtos;


namespace SupportApp.Controllers
    {
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentsController: ControllerBase
        {
        private readonly ICommentService _commentService;
        private readonly ITicketService _ticketService;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController (
            ICommentService commentService,
            ITicketService ticketService,
            ILogger<CommentsController> logger)
            {
            _commentService = commentService;
            _ticketService = ticketService;
            _logger = logger;
            }

        [HttpPost]
        public async Task<IActionResult> Create (CommentCreateDto dto)
            {
            if (string.IsNullOrWhiteSpace(dto.Body))
                return BadRequest("Comment body is required");

            var commentDto = await _commentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = commentDto.Id }, commentDto);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id)
            {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null) return NotFound();

            return Ok(comment);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update (int id, CommentUpdateDto dto)
            {
            await _commentService.UpdateAsync(id, dto);
            return NoContent();
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
            {
            await _commentService.DeleteAsync(id);
            return NoContent();
            }
        }
    }