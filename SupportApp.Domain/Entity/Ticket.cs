using System;
using System.Collections.Generic;

namespace SupportApp.Domain.Entities
    {
    public enum TicketCategory
        {
        Bug,
        FeatureRequest,
        Question
        }

    public enum TicketStatus
        {
        Open,
        InProgress,
        Closed,
        Resolved // Added Resolved for rules
        }

    public enum TicketPriority
        {
        Low,
        Medium,
        High
        }

    public class Ticket
        {
        public int Id { get; set; }

        // Foreign Keys
        public int CustomerId { get; set; }
        public int? AssignedAgentId { get; set; } // Nullable if unassigned

        // Properties with defaults
        public TicketCategory Category { get; set; } = TicketCategory.Bug;
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Customer Customer { get; set; } = null!;
        public Agent? AssignedAgent { get; set; } // Navigation property for agent
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        }
    }