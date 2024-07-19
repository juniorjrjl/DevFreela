using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated;

public record ProjectCreatedNotification(
    string Title,
    decimal TotalCost,
    string FreelancerName,
    string FreelancerEmail,
    string ClientName,
    string ClientEmail
) : INotification;