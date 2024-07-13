using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities;

public class PaginationResultProjectFactory : PaginationResultFactory<Project>
{
    
    protected PaginationResultProjectFactory(): base()
    {
        RuleFor(p => p.Data, f => ProjectFactory.Instance().Generate(f.Random.Number(1, 10)));
    }
    public static PaginationResultProjectFactory Instance() => new();

}