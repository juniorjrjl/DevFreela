using AutoBogus;
using Bogus;
using DevFreela.Application.Queries.GetAllProjects;

namespace DevFreela.UnitTests.Factories
{
    public class GetAllProjectsQueryFactory : AutoFaker<GetAllProjectsQuery>
    {

        public GetAllProjectsQueryFactory()
        {
            Locale = "pt_BR";
            RuleFor(g => g.Query, f => f.Lorem.Word());
        }
    }
}