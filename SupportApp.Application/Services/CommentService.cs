using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Domain.Entities;
using SupportApp.Dtos;

namespace SupportApp.Application.Services
    {
    public class CommentService: ICommentService
        {
        private readonly ICommentRepository _repo;
        private readonly ITicketRepository _ticketRepo;

        public CommentService (ICommentRepository repo, ITicketRepository ticketRepo)
            {
            _repo = repo;
            _ticketRepo = ticketRepo;
            }

        public async Task<CommentReadDto> CreateAsync (CommentCreateDto dto)
            {
            var ticket = await _ticketRepo.GetByIdAsync(dto.TicketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

            if (ticket.Status == TicketStatus.Closed)
                throw new Exception("Cannot comment on Closed ticket");

            var comment = new Comment
                {
                Body = dto.Body,
                TicketId = dto.TicketId,
                CustomerId = dto.CustomerId,
                CreatedAt = DateTime.UtcNow
                };

            await _repo.AddAsync(comment);

            // Update ticket UpdatedAt
            ticket.UpdatedAt = DateTime.UtcNow;
            _ticketRepo.Update(ticket);

            await _repo.SaveChangesAsync();
            await _ticketRepo.SaveChangesAsync();

            return new CommentReadDto
                {
                Id = comment.Id,
                Body = comment.Body,
                TicketId = comment.TicketId,
                CustomerId = (int)comment.CustomerId,
                CreatedAt = comment.CreatedAt
                };
            }

        public async Task<CommentReadDto?> GetByIdAsync (int id)
            {
            var comment = await _repo.GetByIdAsync(id);
            if (comment == null) return null;

            return new CommentReadDto
                {
                Id = comment.Id,
                Body = comment.Body,
                TicketId = comment.TicketId,
                CustomerId = (int)comment.CustomerId,
                CreatedAt = comment.CreatedAt
                };
            }

        public async Task<IReadOnlyList<CommentReadDto>> GetAllAsync ()
            {
            var comments = await _repo.GetAllAsync();
            return comments.Select(c => new CommentReadDto
                {
                Id = c.Id,
                Body = c.Body,
                TicketId = c.TicketId,
                CustomerId = (int)c.CustomerId,
                CreatedAt = c.CreatedAt
                }).ToList();
            }

        public async Task UpdateAsync (int id, CommentUpdateDto dto)
            {
            var comment = await _repo.GetByIdAsync(id);
            if (comment == null)
                throw new Exception("Comment not found");

            comment.Body = dto.Body;
            _repo.Update(comment);
            await _repo.SaveChangesAsync();
            }

        public async Task DeleteAsync (int id)
            {
            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
            }
        }
    }