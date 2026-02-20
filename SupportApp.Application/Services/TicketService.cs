using SupportApp.Application.Abstractions.Persistence;
using SupportApp.Application.Abstractions.Services;
using SupportApp.Domain.Entities;
using SupportApp.Dtos;

namespace SupportApp.Application.Services
    {
    public class TicketService: ITicketService
        {
        private readonly ITicketRepository _repo;

        public TicketService (ITicketRepository repo)
            {
            _repo = repo;
            }

        public async Task<TicketReadDto> CreateAsync (TicketCreateDto dto)
            {
            var ticket = new Ticket
                {
                Title = dto.Title,
                Description = dto.Description,
                CustomerId = dto.CustomerId,
                AssignedAgentId = dto.AgentId,
                Status = TicketStatus.Open,
                Priority = TicketPriority.Medium,
                Category = dto.Category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
                };

            await _repo.AddAsync(ticket);
            await _repo.SaveChangesAsync();

            return new TicketReadDto
                {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category,
                CustomerId = ticket.CustomerId,
                AgentId = ticket.AssignedAgentId,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
                };
            }

        public async Task<TicketReadDto?> GetByIdAsync (int id)
            {
            var ticket = await _repo.GetByIdAsync(id);
            if (ticket == null) return null;

            return new TicketReadDto
                {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category,
                CustomerId = ticket.CustomerId,
                AgentId = ticket.AssignedAgentId,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
                };
            }

        public async Task UpdateAsync (int id, TicketUpdateDto dto)
            {
            var ticket = await _repo.GetByIdAsync(id);
            if (ticket == null) throw new Exception("Ticket not found");

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.UpdatedAt = DateTime.UtcNow;

            _repo.Update(ticket);
            await _repo.SaveChangesAsync();
            }

        public async Task DeleteAsync (int id)
            {
            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
            }

        public async Task AssignAgentAsync (int ticketId, int agentId)
            {
            if (!await _repo.ExistsAsync(ticketId)) throw new Exception("Ticket not found");
            if (!await _repo.CanAssignAgentAsync(ticketId))
                throw new Exception("Cannot assign agent to Closed ticket");

            var ticket = await _repo.GetByIdAsync(ticketId);
            ticket!.AssignedAgentId = agentId;
            ticket.UpdatedAt = DateTime.UtcNow;

            _repo.Update(ticket);
            await _repo.SaveChangesAsync();
            }

        public async Task UpdateStatusAsync (int ticketId, TicketStatus status)
            {
            var ticket = await _repo.GetByIdAsync(ticketId);
            if (ticket == null) throw new Exception("Ticket not found");

            ticket.Status = status;
            ticket.UpdatedAt = DateTime.UtcNow;

            _repo.Update(ticket);
            await _repo.SaveChangesAsync();
            }

        public async Task UpdateCategoryAsync (int ticketId, TicketCategory category)
            {
            if (!await _repo.CanChangeCategoryOrPriorityAsync(ticketId))
                throw new Exception("Cannot change Category for Resolved or Closed ticket");

            var ticket = await _repo.GetByIdAsync(ticketId);
            ticket!.Category = category;
            ticket.UpdatedAt = DateTime.UtcNow;

            _repo.Update(ticket);
            await _repo.SaveChangesAsync();
            }

        public async Task UpdatePriorityAsync (int ticketId, TicketPriority priority)
            {
            if (!await _repo.CanChangeCategoryOrPriorityAsync(ticketId))
                throw new Exception("Cannot change Priority for Resolved or Closed ticket");

            var ticket = await _repo.GetByIdAsync(ticketId);
            ticket!.Priority = priority;
            ticket.UpdatedAt = DateTime.UtcNow;

            _repo.Update(ticket);
            await _repo.SaveChangesAsync();
            }

        public async Task<IReadOnlyList<TicketReadDto>> GetByCustomerIdAsync (int customerId)
            {
            var tickets = await _repo.GetByCustomerIdAsync(customerId);
            return tickets.Select(t => new TicketReadDto
                {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Category = t.Category,
                CustomerId = t.CustomerId,
                AgentId = t.AssignedAgentId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
                }).ToList();
            }

        public async Task<IReadOnlyList<TicketReadDto>> GetByAgentIdAsync (int agentId)
            {
            var tickets = await _repo.GetByAgentIdAsync(agentId);
            return tickets.Select(t => new TicketReadDto
                {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Category = t.Category,
                CustomerId = t.CustomerId,
                AgentId = t.AssignedAgentId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
                }).ToList();
            }
        }
    }