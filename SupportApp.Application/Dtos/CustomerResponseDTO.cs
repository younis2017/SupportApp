namespace SupportApp.Dtos
    {
    // / DTOs for Customer entity
    public class CustomerCreateDto
        {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        }
    // Read DTO includes Id for responses
    public class CustomerReadDto
        {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        }
    // Update DTO for incoming update requests
    public class CustomerUpdateDto
        {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        }
    }