using System.Threading.Tasks;

namespace blasa.access.management.web.Models
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}