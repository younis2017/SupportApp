

namespace SupportApp.Infrastructure.Messaging.IEmail
    {
    public interface IEmailService
        {
        Task SendEmailAsync (string to, string subject, string body);
        }
    }
