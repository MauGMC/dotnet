using Microsoft.AspNetCore.Identity.UI.Services;
namespace Aplicacion.Servicios
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
