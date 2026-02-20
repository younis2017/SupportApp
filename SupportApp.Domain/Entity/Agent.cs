using SupoortApp.Domain.Entity;
using System;
using System.Collections.Generic;

namespace SupportApp.Domain.Entities
    {
    public class Agent
        {
        public int Id { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        // Navigation
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        }
    }