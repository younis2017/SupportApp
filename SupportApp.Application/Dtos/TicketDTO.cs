
using SupportApp.Domain.Entities;

namespace SupportApp.Dtos
    {

    // DTOs for Ticket operations
    public class TicketCreateDto
        {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public TicketCategory Category { get; set; } = TicketCategory.Question;
        }
    // For simplicity, we can have a single read DTO that includes all details. In a real app, you might have separate ones for list vs detail views.
    public class TicketReadDto
        {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TicketStatus Status { get; set; }
        public TicketCategory Category { get; set; }
        public TicketPriority Priority { get; set; }
        public int CustomerId { get; set; }
        public int? AgentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        }
    // For updates, we can have separate DTOs for different aspects of the ticket. This allows partial updates and clearer intent.
    public class TicketUpdateDto
        {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        }
    // For status/category/priority updates, we can have very simple DTOs that just include the new value. This makes it clear that only that field is being updated.
    public class TicketStatusUpdateDto
        {
        public TicketStatus Status { get; set; }
        }
    // Similarly for category and priority updates, we can have simple DTOs that just include the new value. This keeps the API clean and focused.
    public class TicketCategoryUpdateDto
        {
        public TicketCategory Category { get; set; }
        }
    // And for priority updates, we can have a simple DTO that just includes the new priority value. This allows us to update the priority without affecting other fields.
    public class TicketPriorityUpdateDto
        {
        public TicketPriority Priority { get; set; }
        }
    }