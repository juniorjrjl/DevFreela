using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities;

public class PaginationResultProjectFactory : PaginationResultFactory<Project>
{
    
    private static readonly Faker faker = new("pt_BR");
    protected PaginationResultProjectFactory() : 
        base(ProjectFactory.Instance().Generate(faker.Random.Number(1, 10))){}
    public static PaginationResultProjectFactory Instance() => new();

}