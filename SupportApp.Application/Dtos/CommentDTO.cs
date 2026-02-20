namespace SupportApp.Dtos
    {
    // For creating a comment
    public class CommentCreateDto
        {
        public string Body { get; set; } = null!;
        public int TicketId { get; set; }
        public int CustomerId { get; set; }
        }

    // For reading comment
    public class CommentReadDto
        {
        public int Id { get; set; }
        public string Body { get; set; } = null!;
        public int TicketId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        }

    // For updating comment
    public class CommentUpdateDto
        {
        public string Body { get; set; } = null!;
        }
    }