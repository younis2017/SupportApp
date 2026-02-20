using SupoortApp.Domain.Entity;
using System;
using System.Collections.Generic;

namespace SupportApp.Domain.Entities
    {
    public class Customer
        {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }

        // Navigation
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        }
    }