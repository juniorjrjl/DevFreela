using AutoBogus;
using DevFreela.Application.Notification.ProjectCreated;

namespace DevFreela.UnitTests.Factories.DTOs;

public class ProjectCreatedNotificationFactory  : AutoFaker<ProjectCreatedNotification>
{
    private ProjectCreatedNotificationFactory()
    {
        Locale = "pt_BR";
        RuleFor(g => g.Title, f => f.Lorem.Word());
        RuleFor(g => g.TotalCost, f => f.Random.Decimal());
        RuleFor(g => g.FreelancerName, f => f.Name.FullName());
        RuleFor(g => g.FreelancerEmail, f => f.Internet.Email());
        RuleFor(g => g.ClientName, f => f.Name.FullName());
        RuleFor(g => g.ClientEmail, f => f.Internet.Email());
    }

    public static ProjectCreatedNotificationFactory Instance() => new();
}