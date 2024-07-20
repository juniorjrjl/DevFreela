using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Infrastructure.Notification;
using DevFreela.UnitTests.Factories.DTOs;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace DevFreela.UnitTests.Application.Notification;

public class ProjectCreatedNotificationHandlerTest
{
    
    private ProjectCreatedNotificationHandler notificationHandler;

    private INotificationService notificationService;

    public ProjectCreatedNotificationHandlerTest()
    {
        notificationService = Substitute.For<INotificationService>();
        notificationHandler = new (notificationService);
    }

    [Fact]
    public async Task ReceivedProjectInfo_Executed_SendEmail()
    {
        // Arrenge
        var publish = ProjectCreatedNotificationFactory.Instance().Generate();
        var capturedSubject = string.Empty;
        var capturedContent = string.Empty;
        ICollection<MailRecipient> recipients = [];
        notificationService.SendAsync(
            Arg.Do<string>(s => capturedSubject = s),
            Arg.Do<string>(c => capturedContent = c),
            Arg.Do<ICollection<MailRecipient>>(r => recipients = r)
        ).Returns(Task.CompletedTask);
        // Act
        await notificationHandler.Handle(publish, new CancellationToken());
        // Assert
        recipients.Select(r => r.Name).Should().BeEquivalentTo(new List<string>{publish.ClientName, publish.FreelancerName});
        recipients.Select(r => r.Email).Should().BeEquivalentTo(new List<string>{publish.ClientEmail, publish.FreelancerEmail});
        capturedSubject.Should().Contain(publish.Title);
        capturedContent.Should().Contain(publish.Title);
        capturedContent.Should().Contain(publish.TotalCost.ToString());
    }

}