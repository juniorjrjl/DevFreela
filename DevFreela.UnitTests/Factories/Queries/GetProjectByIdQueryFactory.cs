using AutoBogus;
using DevFreela.Application.Queries.GetProjectById;

namespace DevFreela.UnitTests.Factories.Queries;


public class GetProjectByIdQueryFactory :  AutoFaker<GetProjectByIdQuery>
{
    
    private GetProjectByIdQueryFactory()
    {
        RuleFor(p => p.Id, f => f.Random.Int(1, Int32.MaxValue));
    }

    public static GetProjectByIdQueryFactory Instance() => new();

}

