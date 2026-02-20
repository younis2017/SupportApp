

using SupportApp.Domain.Entities;

namespace SupoortApp.Domain.Entity
    {
    public class TicketComment
        {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        // Optional: who wrote it (Agent). If you want both customers + agents later, you can evolve this.
        public int? AuthorAgentId { get; set; }
        public Agent? AuthorAgent { get; set; }
        }
    }
