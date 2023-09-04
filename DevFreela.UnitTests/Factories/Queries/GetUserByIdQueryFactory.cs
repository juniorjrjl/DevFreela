using AutoBogus;
using DevFreela.Application.Queries.GetUserById;

namespace DevFreela.UnitTests.Factories.Queries
{

    public class GetUserByIdQueryFactory :  AutoFaker<GetUserByIdQuery>
    {
        
        private GetUserByIdQueryFactory()
        {
            RuleFor(p => p.Id, f => f.Random.Int(1, Int32.MaxValue));
        }

        public static GetUserByIdQueryFactory Instance() => new();

    }
    
}
