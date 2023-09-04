using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using NSubstitute;
using Xunit;
using Xunit.Sdk;

namespace DevFreela.UnitTests.Application.Queries
{
    
    public class GetAllSkillsCommandHandlerTest
    {

        private readonly GetAllSkillsQueryHandler getAllSkillsQueryHandler;

        private readonly ISkillQueryRepository skillQueryRepository;

        public GetAllSkillsCommandHandlerTest()
        {
            skillQueryRepository = Substitute.For<ISkillQueryRepository>();
            getAllSkillsQueryHandler = new GetAllSkillsQueryHandler(skillQueryRepository);
        }

        [Fact]
        public async void HasThreeProjects_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var skills = SkillFactory.Instance().Generate(3);
            skillQueryRepository.GetAllAsync().Returns(skills);
            // Act
            var actual = await getAllSkillsQueryHandler.Handle(new GetAllSkillsQuery(), new CancellationToken());
            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, skills.Count);
            _ = skillQueryRepository.Received().GetAllAsync();
        }

    }

}
