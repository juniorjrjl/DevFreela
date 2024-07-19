namespace DevFreela.Infrastructure.Notification;

public interface INotificationService
{
    Task SendAsync(string subject, string content, ICollection<MailRecipient> recipients);
}