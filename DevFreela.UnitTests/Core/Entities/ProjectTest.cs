using Bogus;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using DevFreela.UnitTests.Factories;
using Xunit;

namespace DevFreela.UnitTests.Core.Entities
{
    
    public class ProjectTest
    {

        private readonly Faker faker = new Faker("pt_BR");

        [Fact]
        public void WhenProjectCreatedThenStartIt()
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => ProjectStatusEnum.CREATED).Generate();
            project.Start();
            Assert.NotNull(project.StartedAt);
            Assert.Equal(ProjectStatusEnum.IN_PROGRESS, project.Status);
        }

        [Fact]
        public void WhenProjectIsNotCreatedThenThrowError()
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => faker.PickRandomWithout(ProjectStatusEnum.CREATED)).Generate();
            var ex = Assert.Throws<ProjectStatusException>(() => project.Start());
            Assert.Equal($"O projeto {project.Id} não pode ser iniciado porque ele não está no Status 'CREATED'", ex.Message);
        }

        [Fact]
        public void WhenProjectInProgressThenFinishIt()
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => ProjectStatusEnum.IN_PROGRESS).Generate();
            project.Finish();
            Assert.NotNull(project.FinishedAt);
            Assert.Equal(ProjectStatusEnum.FINISHED, project.Status);
        }

        [Fact]
        public void WhenProjectIsNotInProgressThenThrowError()
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => faker.PickRandomWithout(ProjectStatusEnum.IN_PROGRESS)).Generate();
            var ex = Assert.Throws<ProjectStatusException>(() => project.Finish());
            Assert.Equal($"O projeto {project.Id} não pode ser finalizado porque ele não está no Status 'IN_PROGRESS'", ex.Message);
        }

        [Theory]
        [InlineData(ProjectStatusEnum.CREATED)]
        [InlineData(ProjectStatusEnum.IN_PROGRESS)]
        [InlineData(ProjectStatusEnum.SUSPENDED)]
        public void WhenProjectNotCancelledOrFinishedThenCancelIt(ProjectStatusEnum status)
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => status).Generate();
            project.Cancel();
            Assert.Equal(ProjectStatusEnum.CANCELED, project.Status);
        }

        [Fact]
        public void WhenProjectIsCancelledOrFinishedThenThrowError()
        {
            var project = new ProjectFactory().RuleFor(p => p.Status, f => faker.PickRandom(ProjectStatusEnum.CANCELED, ProjectStatusEnum.FINISHED)).Generate();
            var ex = Assert.Throws<ProjectStatusException>(() => project.Cancel());
            Assert.Equal($"O projeto {project.Id} não pode ser cancelado porque está no status '{project.Status}'", ex.Message);
        }

    }

}
