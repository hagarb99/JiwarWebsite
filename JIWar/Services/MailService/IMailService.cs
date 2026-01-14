namespace Jiwar.Services.MailService
{
    public interface IMailService
    {
        Task SendMailAsync(string emailTo, string subject, string body);
    }
}
