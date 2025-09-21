namespace backend.Service.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toAddress, string subject, string body);
    }
}
