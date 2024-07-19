using DevFreela.Infrastructure.Notification;
using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated;

public class ProjectCreatedNotificationHandler(INotificationService notificationService) : INotificationHandler<ProjectCreatedNotification>
{

    private readonly INotificationService _notificationService = notificationService;

    public async Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        var subject = $"Projeto {notification.Title} criado";
        var content = $"O Projeto {notification.Title} foi criado no DevFreela";
        ICollection<MailRecipient> recipients = 
        [
            new(notification.ClientEmail, notification.ClientName),
            new(notification.FreelancerEmail, notification.FreelancerName)
        ];
        await _notificationService.SendAsync(subject, content, recipients);
    }
}