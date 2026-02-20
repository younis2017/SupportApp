using System;

namespace SupportApp.Domain.Entities
    {
    public class Comment
        {
        public int Id { get; set; }

        // FKs
        public int TicketId { get; set; }
        public int? AgentId { get; set; } // nullable
        public int? CustomerId { get; set; } // nullable

        // Properties
        public string Body { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation
        public Ticket Ticket { get; set; } = null!;
        public Agent? Agent { get; set; }
        public Customer? Customer { get; set; }
        }
    }