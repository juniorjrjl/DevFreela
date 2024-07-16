using AutoBogus;
using DevFreela.Application.Queries.GetProjectById;

namespace DevFreela.UnitTests.Factories.Queries;


public class GetProjectByIdQueryFactory :  AutoFaker<GetProjectByIdQuery>
{
    
    private GetProjectByIdQueryFactory()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, int.MaxValue));
    }

    public static GetProjectByIdQueryFactory Instance() => new();

}

