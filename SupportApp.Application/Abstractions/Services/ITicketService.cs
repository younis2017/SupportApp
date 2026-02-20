using SupportApp.Domain.Entities;
using SupportApp.Dtos;

namespace SupportApp.Application.Abstractions.Services
    {
    public interface ITicketService
        {
        Task<TicketReadDto> CreateAsync (TicketCreateDto dto);
        Task<TicketReadDto?> GetByIdAsync (int id);
        Task UpdateAsync (int id, TicketUpdateDto dto);
        Task DeleteAsync (int id);

        // New methods for business logic
        Task AssignAgentAsync (int ticketId, int agentId);
        Task UpdateStatusAsync (int ticketId, TicketStatus status);
        Task UpdateCategoryAsync (int ticketId, TicketCategory category);
        Task UpdatePriorityAsync (int ticketId, TicketPriority priority);

        Task<IReadOnlyList<TicketReadDto>> GetByCustomerIdAsync (int customerId);
        Task<IReadOnlyList<TicketReadDto>> GetByAgentIdAsync (int agentId);
        }
    }