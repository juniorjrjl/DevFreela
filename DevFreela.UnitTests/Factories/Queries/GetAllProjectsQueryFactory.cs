using AutoBogus;
using DevFreela.Application.Queries.GetAllProjects;

namespace DevFreela.UnitTests.Factories.Queries
{
    public class GetAllProjectsQueryFactory : AutoFaker<GetAllProjectsQuery>
    {

        private GetAllProjectsQueryFactory()
        {
            Locale = "pt_BR";
            RuleFor(g => g.Query, f => f.Lorem.Word());
        }

        public static GetAllProjectsQueryFactory Instance() => new();

    }
}
