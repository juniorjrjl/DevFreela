namespace DevFreela.Infrastructure.Notification;

public record MailConfig(string FromName, string FromEmail, string Password, string SmtpHost, int SmtpPort);
